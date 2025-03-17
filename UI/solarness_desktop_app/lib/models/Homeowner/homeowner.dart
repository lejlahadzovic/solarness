import 'package:json_annotation/json_annotation.dart';

part 'homeowner.g.dart';

@JsonSerializable()
class Homeowner {
  int homeownerId;
  String? firstName;
  String? lastName;
  String? phoneNumber;
  String? email;
  String? address;
  String? city;
  String? postalCode;
  String? country;

  Homeowner( this.homeownerId,
    this.firstName,
    this.lastName,
    this.phoneNumber,
    this.email,
    this.address,
    this.city,
    this.postalCode,
    this.country,);

  factory Homeowner.fromJson(Map<String, dynamic> json) =>
      _$HomeownerFromJson(json);

  Map<String, dynamic> toJson() => _$HomeownerToJson(this);
}
