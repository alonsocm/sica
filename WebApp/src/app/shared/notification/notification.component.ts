import { Component, OnInit } from '@angular/core';
import { NotificationModel } from '../models/notification-model';
import { NotificationService } from '../services/notification.service';
import { NotificationType } from '../enums/notification-type';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css'],
})
export class NotificationComponent implements OnInit {
  alert: NotificationModel = { show: false };

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
            type: NotificationType.success,
          });
        }, 5000);
      }
    });
  }

  ngOnInit(): void {}

  close() {
    this.notificationService.updateNotification({
      show: false,
    });
  }
}
