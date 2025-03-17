// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'notification.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Notifications _$NotificationsFromJson(Map<String, dynamic> json) =>
    Notifications(
      (json['id'] as num?)?.toInt(),
      (json['projectId'] as num?)?.toInt(),
      json['title'] as String?,
      json['content'] as String?,
      json['sendDate'] == null
          ? null
          : DateTime.parse(json['sendDate'] as String),
      json['project'] == null
          ? null
          : Project.fromJson(json['project'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$NotificationsToJson(Notifications instance) =>
    <String, dynamic>{
      'id': instance.id,
      'projectId': instance.projectId,
      'title': instance.title,
      'content': instance.content,
      'sendDate': instance.sendDate?.toIso8601String(),
      'project': instance.project,
    };
