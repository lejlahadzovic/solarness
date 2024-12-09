// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'task.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Task _$TaskFromJson(Map<String, dynamic> json) => Task(
      (json['taskId'] as num?)?.toInt(),
      (json['projectId'] as num?)?.toInt(),
      json['taskName'] as String?,
      json['description'] as String?,
      json['startDate'] == null
          ? null
          : DateTime.parse(json['startDate'] as String),
      json['endDate'] == null
          ? null
          : DateTime.parse(json['endDate'] as String),
      json['status'] as String?,
    )
      ..memberId = (json['memberId'] as num?)?.toInt()
      ..project = json['project'] == null
          ? null
          : Project.fromJson(json['project'] as Map<String, dynamic>)
      ..member = json['member'] == null
          ? null
          : TeamMember.fromJson(json['member'] as Map<String, dynamic>);

Map<String, dynamic> _$TaskToJson(Task instance) => <String, dynamic>{
      'taskId': instance.taskId,
      'projectId': instance.projectId,
      'taskName': instance.taskName,
      'description': instance.description,
      'startDate': instance.startDate?.toIso8601String(),
      'endDate': instance.endDate?.toIso8601String(),
      'status': instance.status,
      'memberId': instance.memberId,
      'project': instance.project,
      'member': instance.member,
    };
