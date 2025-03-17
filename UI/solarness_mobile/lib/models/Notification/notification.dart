import 'package:json_annotation/json_annotation.dart';
import 'package:solarness_mobile/models/Project/project.dart';

part 'notification.g.dart';

@JsonSerializable()
class Notifications {
  int? id;
  int? projectId;
  String? title;
  String? content;
  DateTime? sendDate;
  Project? project;

  Notifications( this.id, this.projectId, this.title,this.content, this.sendDate,this.project);

  factory Notifications.fromJson(Map<String, dynamic> json) => _$NotificationsFromJson(json);

  Map<String, dynamic> toJson() => _$NotificationsToJson(this);
}
