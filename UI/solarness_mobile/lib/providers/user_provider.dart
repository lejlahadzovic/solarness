import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:solarness_mobile/models/User/user.dart';

import 'base_provider.dart';

class UserProvider extends BaseProvider<User> {
  UserProvider() : super('User');

  User? _currentUser;
  User? get currentUser => _currentUser;

  @override
  User fromJson(data) {
    // TODO: implement fromJson
    return User.fromJson(data);
  }

  Future<User> getCurrentStudent() async {
    var url = "${baseUrl}${endPoint}/currentUser";
    var uri = Uri.parse(url);

    var response = await http.get(uri, headers: createHeaders());

    if (isValidResponse(response)) {
      var data = jsonDecode(response.body);
      _currentUser = fromJson(data);
      return _currentUser!;
    } else {
      throw Exception("API Error: ${response.statusCode} - ${response.body}");
    }
  }

  Future<bool> changePassword(int id, dynamic request) async {
    var url = "${baseUrl}${endPoint}/$id/change-Password";
    var uri = Uri.parse(url);
    var headers = createHeaders();

    var jsonRequest = jsonEncode(request, toEncodable: myDateSerializer);
    var response = await http.put(uri, headers: headers, body: jsonRequest);

    if (isValidResponse(response)) {
      return true;
    } else {
      return false;
    }
  }

  Future<bool> isUsernameTaken(String username) async {
    var url = "${baseUrl}${endPoint}/check-username/$username";
    var uri = Uri.parse(url);

    var response = await http.get(uri, headers: createHeaders());

    if (response.statusCode == 200) {
      return false;
    } else {
      return true;
    }
  }
}
