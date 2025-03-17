// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'homeowner.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Homeowner _$HomeownerFromJson(Map<String, dynamic> json) => Homeowner(
      (json['homeownerId'] as num).toInt(),
      json['firstName'] as String?,
      json['lastName'] as String?,
      json['phoneNumber'] as String?,
      json['email'] as String?,
      json['address'] as String?,
      json['city'] as String?,
      json['postalCode'] as String?,
      json['country'] as String?,
    );

Map<String, dynamic> _$HomeownerToJson(Homeowner instance) => <String, dynamic>{
      'homeownerId': instance.homeownerId,
      'firstName': instance.firstName,
      'lastName': instance.lastName,
      'phoneNumber': instance.phoneNumber,
      'email': instance.email,
      'address': instance.address,
      'city': instance.city,
      'postalCode': instance.postalCode,
      'country': instance.country,
    };
