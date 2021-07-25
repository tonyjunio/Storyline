var bsMainOffCanvas = null;

function ToggleMainOffCanvas() {

    if (bsMainOffCanvas == null) {
        var mainOffCanvas = document.getElementById('mainOffCanvas');
        bsMainOffCanvas = new bootstrap.Offcanvas(mainOffCanvas);
    }

    bsMainOffCanvas.toggle();
}