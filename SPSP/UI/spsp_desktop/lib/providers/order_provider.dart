import 'package:spsp_desktop/models/order.dart';
import 'package:spsp_desktop/providers/base_provider.dart';

class OrderProvider extends BaseProvider<Order> {
  OrderProvider() : super("Order");

  @override
  Order fromJson(data) {
    return Order.fromJson(data);
  }
}
