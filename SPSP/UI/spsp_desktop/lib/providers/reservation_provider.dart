import 'package:spsp_desktop/models/reservation/reservation.dart';
import 'package:spsp_desktop/providers/base_provider.dart';

class ReservationProvider extends BaseProvider<Reservation> {
  ReservationProvider() : super("Reservation");

  @override
  Reservation fromJson(data) {
    return Reservation.fromJson(data);
  }
}
