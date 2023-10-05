import { NotificationType } from './notification-type';

export interface NotificationModel {
  show: boolean;
  type?: NotificationType;
  text?: string;
}
