import 'package:json_annotation/json_annotation.dart';
import 'package:solarness_desktop_app/models/Role/role.dart';

part 'user.g.dart';

@JsonSerializable()
class User {
  int? userId;
  int? roleId;
  String? firstName;
  String? lastName;
  String? username;
  String? email;
  String? picture;
  Role? role;

  User(this.userId, this.firstName, this.lastName, this.username, this.email, this.picture);

  factory User.fromJson(Map<String, dynamic> json) =>
      _$UserFromJson(json);

  Map<String, dynamic> toJson() => _$UserToJson(this);
}
