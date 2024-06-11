import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  @ViewChild('canvas', { static: true }) myCanvas!: ElementRef;
  ngOnInit(): void {
    const canvas: HTMLCanvasElement = this.myCanvas.nativeElement;
    const context = canvas.getContext('2d');
    if (context) {
      context.strokeStyle = 'red';
      context.fillStyle = 'rgba(17, 0, 255, 0.5)';
      this.#useGradients(context);
      this.#drawRectangle(context);
      this.#drawTriangle(context);
      this.#drawArc(context);
      this.#drawCurve(context);
      this.#drawUsingPath(context);
      this.#drawLine(context);
      this.#drawText(context);
    }
  }

  #drawRectangle(context: CanvasRenderingContext2D) {
    context.fillRect(20, 20, 100, 100);
    context.clearRect(40, 40, 30, 30);
    context.strokeRect(50, 50, 10, 10);
  }
  #drawTriangle(context: CanvasRenderingContext2D) {
    context.beginPath();
    context.moveTo(150, 70);
    context.lineTo(200, 20);
    context.lineTo(200, 120);
    context.fill();
    // context.closePath();
    // context.stroke();
  }
  #drawArc(context: CanvasRenderingContext2D) {
    context.beginPath();
    context.arc(300, 100, 80, (Math.PI / 180) * 0, (Math.PI / 180) * 360);
    context.stroke();
    // context.fill();
  }
  #drawCurve(context: CanvasRenderingContext2D) {
    context.beginPath();
    context.moveTo(500, 200);
    context.quadraticCurveTo(550, 0, 600, 200);
    context.stroke();
    context.beginPath();
    context.moveTo(700, 200);
    context.bezierCurveTo(750, 0, 750, 100, 800, 200);
    context.stroke();
  }

  #drawUsingPath(context: CanvasRenderingContext2D) {
    context.lineWidth = 20;
    context.lineJoin = 'bevel';
    const rectangle = new Path2D();
    rectangle.rect(20, 150, 100, 100);
    context.stroke(rectangle);
    const circle = new Path2D();
    circle.arc(300, 300, 80, (Math.PI / 180) * 0, (Math.PI / 180) * 360);
    context.fill(circle);
  }
  #drawLine(context: CanvasRenderingContext2D) {
    context.lineWidth = 10;
    context.lineCap = 'round';
    context.setLineDash([4, 4]);
    context.lineDashOffset = 0;
    context.beginPath();
    context.moveTo(100, 600);
    context.lineTo(200, 600);
    context.stroke();
  }
  #drawText(context: CanvasRenderingContext2D) {
    context.shadowOffsetX = 4;
    context.shadowOffsetY = 4;
    context.shadowBlur = 3;
    context.shadowColor = 'rgba(0, 0, 0, 0.5)';
    context.fillStyle = 'black';
    context.font = '48px Arial';
    context.fillText('Hello', 100, 500);
  }
  #useGradients(context: CanvasRenderingContext2D) {
    const lineargradient = context.createLinearGradient(20, 20, 120, 120);
    lineargradient.addColorStop(0, 'white');
    lineargradient.addColorStop(1, 'black');
    context.fillStyle = lineargradient;
    const radgrad = context.createRadialGradient(300, 300, 40, 300, 300, 80);
    radgrad.addColorStop(0, '#A7D30C');
    radgrad.addColorStop(0.9, '#019F62');
    radgrad.addColorStop(1, 'rgba(1, 159, 98, 0.5)');
    context.fillStyle = radgrad;
    const conicGrad = context.createConicGradient((Math.PI / 180) * 0, 150, 70);
    conicGrad.addColorStop(0, '#A7D30C');
    conicGrad.addColorStop(1, '#fff');
    context.fillStyle = conicGrad;
  }
}
