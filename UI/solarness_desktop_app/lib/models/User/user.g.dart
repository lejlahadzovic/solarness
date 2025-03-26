// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

User _$UserFromJson(Map<String, dynamic> json) => User(
      (json['userId'] as num?)?.toInt(),
      json['firstName'] as String?,
      json['lastName'] as String?,
      json['username'] as String?,
      json['email'] as String?,
      json['picture'] as String?,
    )
      ..roleId = (json['roleId'] as num?)?.toInt()
      ..role = json['role'] == null
          ? null
          : Role.fromJson(json['role'] as Map<String, dynamic>);

Map<String, dynamic> _$UserToJson(User instance) => <String, dynamic>{
      'userId': instance.userId,
      'roleId': instance.roleId,
      'firstName': instance.firstName,
      'lastName': instance.lastName,
      'username': instance.username,
      'email': instance.email,
      'picture': instance.picture,
      'role': instance.role,
    };
