// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'team_memeber.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

TeamMember _$TeamMemberFromJson(Map<String, dynamic> json) => TeamMember(
      (json['memberId'] as num?)?.toInt(),
      (json['userId'] as num?)?.toInt(),
      (json['teamId'] as num?)?.toInt(),
      json['team'] == null
          ? null
          : Team.fromJson(json['team'] as Map<String, dynamic>),
      json['user'] == null
          ? null
          : User.fromJson(json['user'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$TeamMemberToJson(TeamMember instance) =>
    <String, dynamic>{
      'memberId': instance.memberId,
      'userId': instance.userId,
      'teamId': instance.teamId,
      'team': instance.team,
      'user': instance.user,
    };
