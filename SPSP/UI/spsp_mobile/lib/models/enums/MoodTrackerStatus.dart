enum MoodTrackerStatusEnum {
  HAPPY,
  SAD,
  STRESSED,
  EXCITED,
  TIRED,
}

extension MoodTrackerStatusExtension on MoodTrackerStatusEnum {
  String get value {
    switch (this) {
      case MoodTrackerStatusEnum.HAPPY:
        return 'Sretan';
      case MoodTrackerStatusEnum.SAD:
        return 'Tužan';
      case MoodTrackerStatusEnum.STRESSED:
        return 'Pod stresom';
      case MoodTrackerStatusEnum.EXCITED:
        return 'Uzbuđen';
      case MoodTrackerStatusEnum.TIRED:
        return 'Umoran';
    }
  }
}

final Map<String, MoodTrackerStatusEnum> _stringToEnum = {
  'Sretan': MoodTrackerStatusEnum.HAPPY,
  'Tužan': MoodTrackerStatusEnum.SAD,
  'Pod stresom': MoodTrackerStatusEnum.STRESSED,
  'Uzbuđen': MoodTrackerStatusEnum.EXCITED,
  'Umoran': MoodTrackerStatusEnum.TIRED,
};

MoodTrackerStatusEnum enumFromString(String? value) {
  if (value == null || !_stringToEnum.containsKey(value)) {
    throw ArgumentError('Invalid value: $value');
  }
  return _stringToEnum[value]!;
}
