// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'mood_tracker.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

MoodTracker _$MoodTrackerFromJson(Map<String, dynamic> json) => MoodTracker(
      json['id'] as int?,
      json['dodatniOpis'] as String?,
      DateTime.parse(json['datumEvidencije'] as String),
      json['moodTrackerStatus'] as String,
      json['customerId'] as int?,
      json['customer'] == null
          ? null
          : Customer.fromJson(json['customer'] as Map<String, dynamic>),
      json['valid'] as bool?,
    );

Map<String, dynamic> _$MoodTrackerToJson(MoodTracker instance) =>
    <String, dynamic>{
      'id': instance.id,
      'dodatniOpis': instance.dodatniOpis,
      'datumEvidencije': instance.datumEvidencije.toIso8601String(),
      'moodTrackerStatus': instance.moodTrackerStatus,
      'customerId': instance.customerId,
      'customer': instance.customer,
      'valid': instance.valid,
    };
