// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'projectstatus.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ProjectStatus _$ProjectStatusFromJson(Map<String, dynamic> json) =>
    ProjectStatus(
      (json['statusId'] as num?)?.toInt(),
      json['statusName'] as String?,
    );

Map<String, dynamic> _$ProjectStatusToJson(ProjectStatus instance) =>
    <String, dynamic>{
      'statusId': instance.statusId,
      'statusName': instance.statusName,
    };
