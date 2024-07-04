enum ReservationStatus {
  PENDING_CONFIRMATION,
  ON_HOLD,
  CONFIRMED,
  CANCELED,
}

extension ReservationStatusExtension on ReservationStatus {
  String get value {
    switch (this) {
      case ReservationStatus.PENDING_CONFIRMATION:
        return 'Na čekanju potvrde';
      case ReservationStatus.ON_HOLD:
        return 'Na čekanju';
      case ReservationStatus.CONFIRMED:
        return 'Potvrđeno';
      case ReservationStatus.CANCELED:
        return 'Otkazano';
    }
  }

  static ReservationStatus enumFromString(String value) {
    if (!_stringToEnum.containsKey(value)) {
      throw ArgumentError('Invalid value: $value');
    }
    return _stringToEnum[value]!;
  }
}

final Map<String, ReservationStatus> _stringToEnum = {
  'PENDING_CONFIRMATION': ReservationStatus.PENDING_CONFIRMATION,
  'ON_HOLD': ReservationStatus.ON_HOLD,
  'CONFIRMED': ReservationStatus.CONFIRMED,
  'CANCELED': ReservationStatus.CANCELED,
};
