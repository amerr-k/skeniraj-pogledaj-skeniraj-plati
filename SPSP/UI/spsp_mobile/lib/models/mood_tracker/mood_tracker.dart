import 'package:json_annotation/json_annotation.dart';
import 'package:spsp_mobile/models/customer.dart';

part 'mood_tracker.g.dart';

@JsonSerializable()
class MoodTracker {
  int? id;
  String? dodatniOpis;
  DateTime datumEvidencije;
  String moodTrackerStatus;
  int? customerId;
  Customer? customer;
  bool? valid;

  MoodTracker(this.id, this.dodatniOpis, this.datumEvidencije, this.moodTrackerStatus, this.customerId, this.customer,
      this.valid);

  /// A necessary factory constructor for creating a new User instance
  /// from a map. Pass the map to the generated `_$UserFromJson()` constructor.
  /// The constructor is named after the source class, in this case, User.
  factory MoodTracker.fromJson(Map<String, dynamic> json) => _$MoodTrackerFromJson(json);

  /// `toJson` is the convention for a class to declare support for serialization
  /// to JSON. The implementation simply calls the private, generated
  /// helper method `_$UserToJson`.
  Map<String, dynamic> toJson() => _$MoodTrackerToJson(this);
}
