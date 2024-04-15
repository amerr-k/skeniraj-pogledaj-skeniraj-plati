import 'package:flutter/material.dart';
import 'package:spsp_mobile/models/enums/OrderStatus.dart';
import 'package:spsp_mobile/models/order.dart';
import 'package:spsp_mobile/models/search_result.dart';
import 'package:spsp_mobile/providers/order_provider.dart';

class OrderListCheckOutCustomerScreen extends StatefulWidget {
  final String? qrTableId;

  const OrderListCheckOutCustomerScreen({Key? key, required this.qrTableId})
      : super(key: key);

  @override
  _OrderListCheckOutCustomerScreenState createState() =>
      _OrderListCheckOutCustomerScreenState();
}

class _OrderListCheckOutCustomerScreenState
    extends State<OrderListCheckOutCustomerScreen> {
  RequestResult<Order>? orders;
  List<bool> _isChecked = [];

  @override
  void initState() {
    super.initState();
    _getOrders();
  }

  Future<void> _getOrders() async {
    final orderProvider = OrderProvider();
    final fetchedOrders = await orderProvider.get(filter: {
      "qrTableId": widget.qrTableId,
      "orderStatus": OrderStatus.ACTIVE.name,
      "isOrderItemsIncluded": true
    });
    setState(() {
      orders = fetchedOrders;
      _isChecked = List<bool>.filled(fetchedOrders?.result.length ?? 0, false);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Lista narudžbi'),
      ),
      body: orders != null ? _buildOrderList() : CircularProgressIndicator(),
      bottomNavigationBar: BottomAppBar(
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          children: [
            ElevatedButton(
              onPressed: () {
                _payCheckedOrders();
              },
              child: Text('Plati odabrano'),
            ),
            ElevatedButton(
              onPressed: () {
                _payAllOrders();
              },
              child: Text('Plati sve'),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildOrderList() {
    return ListView.builder(
      itemCount: orders!.result.length,
      itemBuilder: (context, index) {
        final order = orders!.result[index];
        return Column(
          children: [
            CheckboxListTile(
              title: Text('ID narudžbe: ${order.id}'),
              subtitle: Text('Ukupno: ${order.totalAmountWithVAT}'),
              value: _isChecked[index],
              onChanged: (value) {
                setState(() {
                  _isChecked[index] = value!;
                });
              },
            ),
            ExpansionTile(
              title: Text('Detalji',
                  style: TextStyle(fontSize: 12)), // Adjust the font size as needed
              children: order.orderItems.map((item) {
                return Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text('Artikal: ${item.menuItem!.name}'),
                      Text('Količina: ${item.quantity}'),
                      Text('Cijena: ${item.subtotal}'),
                    ],
                  ),
                );
              }).toList(),
            ),
          ],
        );
      },
    );
  }

  void _payCheckedOrders() {
    final checkedOrders = <Order>[];
    for (int i = 0; i < orders!.result.length; i++) {
      if (_isChecked[i]) {
        checkedOrders.add(orders!.result[i]);
      }
    }
    // Perform payment logic with checkedOrders
  }

  void _payAllOrders() {
    // Perform payment logic with orders!.result
  }
}
