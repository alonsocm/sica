import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { NotificationModel } from '../models/notification-model';
import { NotificationType } from '../models/notification-type';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private notificationDataSource = new BehaviorSubject<NotificationModel>({
    show: false,
    type: NotificationType.success,
    text: '',
  });

  public notification = this.notificationDataSource.asObservable();

  updateNotification(value: NotificationModel) {
    this.notificationDataSource.next(value);
  }

  constructor() {}
}
