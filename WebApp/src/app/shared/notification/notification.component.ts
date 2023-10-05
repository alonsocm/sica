import { Component, OnInit } from '@angular/core';
import { NotificationModel } from '../models/notification-model';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css'],
})
export class NotificationComponent implements OnInit {
  alert: NotificationModel = { text: '', type: '', show: false };

  constructor(private notificationService: NotificationService) {
    this.notificationService.notification.subscribe((notification) => {
      this.alert.show = notification.show;
      this.alert.text = notification.text;
      this.alert.type = notification.type;

      if (this.alert.show) {
        setTimeout(() => {
          this.notificationService.updateNotification({
            show: false,
            text: '',
            type: '',
          });
        }, 10000);
      }
    });
  }

  ngOnInit(): void {}

  close() {
    this.notificationService.updateNotification({
      show: false,
      text: '',
      type: '',
    });
  }
}
