import 'package:spsp_mobile/models/qr_table.dart';
import 'package:spsp_mobile/providers/base_provider.dart';

class QRTableProvider extends BaseProvider<QRTable> {
  QRTableProvider() : super("QRTable");

  @override
  QRTable fromJson(data) {
    return QRTable.fromJson(data);
  }
}
