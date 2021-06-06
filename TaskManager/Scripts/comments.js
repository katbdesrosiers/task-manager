let slider = document.getElementById("slider");
let output = document.getElementById("percent");
output.innerHTML = slider.value;

slider.oninput = function () {
    output.innerHTML = this.value;
}

let urgentButton = document.getElementById("urgent-button");
let urgentComment = document.getElementById("urgent-comment");
let noComments = document.getElementById("no-comments");

urgentButton.onclick = function () {
    urgentComment.style.display = "block";
    noComments.style.display = "none";
}