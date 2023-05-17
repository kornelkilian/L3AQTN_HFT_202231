fetch('http://localhost:10615/bus')
    .then(x => x.json())
    .then(y => console.log(y));


let buses = [];
let connection = null;
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
            `<button type="button" onclick="remove(${t.id})">Delete</button>`
            + "</td></tr>";
    });
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