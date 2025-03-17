import 'package:solarness_mobile/models/Notification/notification.dart';

import 'base_provider.dart';

class NotificationProvider extends BaseProvider<Notifications> {
  NotificationProvider() : super('Notification');

  @override
  Notifications fromJson(data) {
    // TODO: implement fromJson
    return Notifications.fromJson(data);
  }

}
