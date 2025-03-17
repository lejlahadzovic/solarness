import '../models/Homeowner/homeowner.dart';
import 'base_provider.dart';

class HomeownerProvider extends BaseProvider<Homeowner> {
  HomeownerProvider() : super('Homeowner');

  @override
  Homeowner fromJson(data) {
    // TODO: implement fromJson
    return Homeowner.fromJson(data);
  }

}
