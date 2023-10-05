import { NotificationType } from '../enums/notification-type';

export interface NotificationModel {
  show: boolean;
  type?: NotificationType;
  text?: string;
}
