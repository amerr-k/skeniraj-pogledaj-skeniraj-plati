import 'package:spsp_mobile/models/mood_tracker/mood_tracker.dart';
import 'package:spsp_mobile/providers/base_provider.dart';

class MoodTrackerProvider extends BaseProvider<MoodTracker> {
  MoodTrackerProvider() : super("MoodTracker");

  @override
  MoodTracker fromJson(data) {
    return MoodTracker.fromJson(data);
  }
}
