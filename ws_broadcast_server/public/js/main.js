let ws = new WebSocket('ws://localhost:8080/'); // please change here to your environment
let alpha = 0, beta = 0, gamma = 0;

window.addEventListener("deviceorientation", (dat) => {
  alpha = dat.alpha;
  beta  = dat.beta;
  gamma = dat.gamma;
});

ws.addEventListener('open', (event) => {
  console.log("connected.");
});

let send = (msg) => {
  if (ws && ws.readyState == WebSocket.OPEN) {
    ws.send(msg);
  }
}

let timer = window.setInterval(() => {
  let msg = `x${alpha}y${beta}z${gamma}`;
  send(msg);
  displayData();
}, 33);

function displayData() {
  var txt = document.getElementById("txt");
  txt.innerHTML = "alpha: " + alpha + "<br>"
    + "beta:  " + beta  + "<br>"
    + "gamma: " + gamma
}