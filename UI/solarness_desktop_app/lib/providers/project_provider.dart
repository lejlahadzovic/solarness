import 'dart:io';

import '../models/Project/project.dart';
import 'base_provider.dart';
import 'package:http/http.dart' as http;

class ProjectProvider extends BaseProvider<Project> {
  ProjectProvider() : super('Project');

  @override
  Project fromJson(data) {
    // TODO: implement fromJson
    return Project.fromJson(data);
    
  }
 /// Calls the API to predict energy and downloads the resulting file.
  Future<File?> predictEnergy() async {
    try {
var url = "${baseUrl}${endPoint}/predict-energy";
    var uri = Uri.parse(url);

    var headers = createHeaders();

    var response = await http.post(uri, headers: headers);

      if (response.statusCode == 200) {
        final directory ="C:/Users/Lejla/Downloads";
        final filePath = "$directory/prediction_result.txt";
        final file = File(filePath);

        await file.writeAsString(response.body);

        return file;
      } else {
        throw Exception("Failed to download prediction file: ${response.statusCode}");
      }
    } catch (e) {
      throw Exception("Error in predictEnergy: $e");
    }
  }
}
