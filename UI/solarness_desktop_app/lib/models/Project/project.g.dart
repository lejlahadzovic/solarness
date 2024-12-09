// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'project.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Project _$ProjectFromJson(Map<String, dynamic> json) => Project(
      (json['projectId'] as num?)?.toInt(),
      json['projectName'] as String?,
      json['projectDescription'] as String?,
      json['streetAddress'] as String?,
      json['city'] as String?,
      (json['kw'] as num?)?.toInt(),
      json['siteInspectionDate'] == null
          ? null
          : DateTime.parse(json['siteInspectionDate'] as String),
      json['engineeringSubmitDate'] == null
          ? null
          : DateTime.parse(json['engineeringSubmitDate'] as String),
      json['engineeringReceivedDate'] == null
          ? null
          : DateTime.parse(json['engineeringReceivedDate'] as String),
      json['saleDate'] == null
          ? null
          : DateTime.parse(json['saleDate'] as String),
      json['significance'] as String?,
      json['urgency'] as String?,
      json['priorityLevel'] as String?,
      (json['statusId'] as num?)?.toInt(),
      (json['teamId'] as num?)?.toInt(),
      json['status'] == null
          ? null
          : ProjectStatus.fromJson(json['status'] as Map<String, dynamic>),
      json['team'] == null
          ? null
          : Team.fromJson(json['team'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$ProjectToJson(Project instance) => <String, dynamic>{
      'projectId': instance.projectId,
      'projectName': instance.projectName,
      'projectDescription': instance.projectDescription,
      'streetAddress': instance.streetAddress,
      'city': instance.city,
      'kw': instance.kw,
      'siteInspectionDate': instance.siteInspectionDate?.toIso8601String(),
      'engineeringSubmitDate':
          instance.engineeringSubmitDate?.toIso8601String(),
      'engineeringReceivedDate':
          instance.engineeringReceivedDate?.toIso8601String(),
      'saleDate': instance.saleDate?.toIso8601String(),
      'significance': instance.significance,
      'urgency': instance.urgency,
      'priorityLevel': instance.priorityLevel,
      'statusId': instance.statusId,
      'teamId': instance.teamId,
      'team': instance.team,
      'status': instance.status,
    };
