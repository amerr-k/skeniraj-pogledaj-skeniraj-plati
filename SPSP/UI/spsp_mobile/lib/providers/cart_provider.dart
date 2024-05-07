import 'package:collection/collection.dart';
import 'package:flutter/widgets.dart';
import 'package:spsp_mobile/models/cart.dart';
import 'package:spsp_mobile/models/menu_item.dart';
import 'package:spsp_mobile/models/qr_table.dart';

class QRTableSelectorProvider with ChangeNotifier {
  // Cart cart = Cart();
  late QRTable? selectedQRTable = null;
  late DateTime? reservationDate = null;

  setSelectedQRTable(QRTable? newQRTable) {
    selectedQRTable = newQRTable;
    notifyListeners();
  }

  setReservationDate(DateTime? reservationDateString) {
    reservationDate = reservationDateString;
    selectedQRTable = null;
    notifyListeners();
  }

  // addToCart(MenuItem newOrderItem) {
  //   cart.totalAmountWithVAT += newOrderItem.price!;
  //   if (findInCart(newOrderItem) != null) {
  //     var oldOrderItem = findInCart(newOrderItem);
  //     oldOrderItem?.quantity++;
  //     oldOrderItem?.subtotal += newOrderItem.price!;
  //   } else {
  //     cart.items.add(CartItem(newOrderItem, 1, newOrderItem.price!));
  //   }

  //   notifyListeners();
  // }

  // removeFromCart(CartItem cartItem) {
  //   cart.totalAmountWithVAT -= (cartItem.menuItem.price! * cartItem.quantity);
  //   cart.items.removeWhere((item) => item.menuItem.id == cartItem.menuItem.id);
  //   notifyListeners();
  // }

  // CartItem? findInCart(MenuItem product) {
  //   CartItem? item =
  //       cart.items.firstWhereOrNull((item) => item.menuItem.id == product.id);
  //   return item;
  // }
}
