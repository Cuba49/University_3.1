window.onload = function () {
    var canvas = document.getElementById("workfield");
    var painter = new Painter(canvas);

    canvas.addEventListener("mousedown", function (e) {
        painter.startDrawing(e.offsetX, e.offsetY);
    });
    canvas.addEventListener("mouseup", function (e) {
        painter.stopDrawing();
    });
    canvas.addEventListener("mousemove", function (e) {
        painter.draw(e.offsetX, e.offsetY);
    });

    var toolsArray = document.getElementsByName("tool");
    for (var i = 0; i < toolsArray.length; i++) {
        toolsArray[i].onchange = function (e) {
            painter.selectedTool = e.srcElement.value;
            console.log(painter.selectedTool);
        };
    }
    painter.selectedTool = toolsArray[0].value;

    var strokeColorInput = document.getElementById('stroke-color');
    strokeColorInput.onchange = function (e) {
        painter.getBrush().color = strokeColorInput.value;
    };

    var thicknessInput = document.getElementById('tool-thickness');
    thicknessInput.onchange = function (e) {
        painter.getBrush().radius = Number(thicknessInput.value);
    };
};

var Brush = (function () {
    function Brush(radius, color) {
        this.radius = radius;
        this.color = color;
    }
    return Brush;
})();

var Painter = (function () {
    function Painter(canvas) {
        this._brush = undefined;
        this._isPainting = false;
        this.xPosition = undefined;
        this.yPosition = undefined;
        this.selectedTool = undefined;
        this._canvas = canvas;
        this._context = this._canvas.getContext("2d");
    }
    Painter.prototype.getBrush = function () {
        return this._brush;
    };

    Painter.prototype.startDrawing = function (xPosition, yPosition) {
        this._isPainting = true;
        this.xPosition = xPosition;
        this.yPosition = yPosition;
    };

    Painter.prototype.stopDrawing = function () {
        this._isPainting = false;
        this.xPosition = undefined;
        this.yPosition = undefined;

        this._context.globalCompositeOperation = "source-over";
        this._imageData = this.getImageDataFromContext();
    };

    Painter.prototype.draw = function (newX, newY) {
        if (!this._isPainting)
            return;
        this.refreshBrush();

        this._context.beginPath();
        switch (this.selectedTool) {
            case "pencil":
                this.drawPencil(newX, newY);

                this.xPosition = newX;
                this.yPosition = newY;
                break;
            case "line": {
                if (this._imageData !== undefined) {
                    this._context.putImageData(this._imageData, 0, 0);
                }
                this.drawLine(newX, newY);
                this._imageData = this.getImageDataFromContext();
                break;
            }
            case "rectangle": {
                if (this._imageData !== undefined) {
                    this._context.putImageData(this._imageData, 0, 0);
                }
                this.drawRectangle(newX, newY);
                this._imageData = this.getImageDataFromContext();
                break;
            }
                case "circle":{
       if(this._imageData !== undefined){
           this._context.putImageData(this._imageData, 0, 0);
         }
         this.drawCircle(newX, newY);
        this._imageData = this.getImageDataFromContext();
        break;
     }

            case "eraser": {
                this.erase(newX, newY);

                this.xPosition = newX;
                this.yPosition = newY;
                break;
            }
            default:
                console.log('invalid tool: ' + this.selectedTool);
                break;
        }
        this._context.stroke();
    };
    Painter.prototype.refreshBrush = function () {
        if (this._brush === undefined) {
            this._brush = new Brush(1, "#000");
        }

        this._context.strokeStyle = this._brush.color;
        this._context.lineWidth = this._brush.radius;
    };

    Painter.prototype.drawPencil = function (newX, newY) {
        this._context.beginPath();
        this._context.moveTo(this.xPosition, this.yPosition);
        this._context.lineTo(newX, newY);
        this._context.stroke();
    };

    Painter.prototype.drawLine = function (newX, newY) {
        this._context.moveTo(this.xPosition, this.yPosition);
        this._context.lineTo(newX, newY);
    };

    Painter.prototype.drawRectangle = function (newX, newY) {
        var width = newX - this.xPosition;
        var height = newY - this.yPosition;
        this._context.rect(this.xPosition, this.yPosition, width, height);
    };

    Painter.prototype.drawCircle = function (newX, newY) {
        var width = Math.abs(newX - this.xPosition);
        var height = Math.abs(newY - this.yPosition);
        this._context.ellipse(this.xPosition, this.yPosition, width, height, 2 * Math.PI, 0, 8);
    };

    Painter.prototype.erase = function (newX, newY) {
        this._context.globalCompositeOperation = "destination-out";
        this._context.beginPath();
        this._context.moveTo(this.xPosition, this.yPosition);
        this._context.lineTo(newX, newY);
        this._context.stroke();
    };

    Painter.prototype.getImageDataFromContext = function () {
        return this._context.getImageData(0, 0, this._canvas.width, this._canvas.height);
    };
    return Painter;
})();
