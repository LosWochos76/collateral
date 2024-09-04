var connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5131/chat").build();

connection.start().then(function() {
    console.log("Connection to SignalR Hub established!");
}).catch(function(err) {
    return console.error(err.toString());
});

connection.on("ReceiveMessage", function(received) {
    var li = document.createElement("li");
    var messages = document.getElementById("messages");

    if (messages.firstChild) {
        messages.insertBefore(li, messages.firstChild);
    } else {
        messages.appendChild(li);
    }
    
    li.textContent = `From '${received.from}': ${received.body}`;
});

document.getElementById("sendButton").addEventListener("click", function(event) {
    var messageModel = {
        from: document.getElementById("from").value,
        body: document.getElementById("body").value
    };

    connection.invoke("SendMessage", messageModel).catch(function(err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});