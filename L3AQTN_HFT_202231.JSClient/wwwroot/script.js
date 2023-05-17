fetch('http://localhost:10615/bus')
    .then(x => x.json())
    .then(y => console.log(y));

document.getElementById('updateformdiv').style.display = 'none';

let buses = [];
let connection = null;
let busIdToUpdate = -1;
getdata();
setupSignalR();



function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:10615/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BusCreated", (user, message) => {
        getdata();
    });

    connection.on("BusDeleted", (user, message) => {
        getdata();
    });
    connection.on("BusUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:10615/bus')
        .then(x => x.json())
        .then(y => {
            buses = y;
            //console.log(actors);
            display();
        });
}

function display() {

    document.getElementById('resultarea').innerHTML = "";
    buses.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.model + "</td><td>" +
        `<button type="button" onclick="remove(${t.id})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.id})">Update</button>`

            + "</td></tr>";
    });
}

function showupdate(id) {
    document.getElementById('modelnametoupdate').value = buses.find(t => t['id'] == id)['model'];
    document.getElementById('pricetoupdate').value = buses.find(t => t['id'] == id)['price'];
    document.getElementById('brandidtoupdate').value = buses.find(t => t['id'] == id)['brandId'];
    document.getElementById('owneridtoupdate').value = buses.find(t => t['id'] == id)['ownerId'];

    document.getElementById('updateformdiv').style.display = 'flex';
    busIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('modelnametoupdate').value;
    let price_ = document.getElementById('pricetoupdate').value;
    let brand = document.getElementById('brandidtoupdate').value;
    let owner = document.getElementById('owneridtoupdate').value;

    fetch('http://localhost:10615/bus', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                id:busIdToUpdate,
                model: name,
                price: price_,
                ownerId: owner,
                brandId: brand,
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}


function remove(id) {
    fetch('http://localhost:10615/bus/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('modelname').value;
    let price_ = document.getElementById('price').value;
    let brand = document.getElementById('brandid').value;
    let owner = document.getElementById('ownerid').value;

    fetch('http://localhost:10615/bus', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                model: name,
                price: price_,
                ownerId: owner,
                brandId: brand,
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}