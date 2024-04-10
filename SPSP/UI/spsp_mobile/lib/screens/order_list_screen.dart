// ignore_for_file: prefer_const_constructors, prefer_const_literals_to_create_immutables

import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

import 'package:pdf/pdf.dart';
import 'package:spsp_mobile/models/enums/OrderStatus.dart';
import 'package:spsp_mobile/models/invoice.dart';
import 'package:spsp_mobile/models/order.dart';
import 'package:spsp_mobile/models/qr_table.dart';
import 'package:spsp_mobile/models/search_result.dart';
import 'package:spsp_mobile/models/supplier.dart';
import 'package:spsp_mobile/providers/order_provider.dart';
import 'package:spsp_mobile/providers/qr_table_provider.dart';
import 'package:spsp_mobile/utils/util.dart';
import 'package:spsp_mobile/widgets/master_screen.dart';

class OrderListScreen extends StatefulWidget {
  const OrderListScreen({super.key});

  @override
  State<OrderListScreen> createState() => _OrderListScreenState();
}

class _OrderListScreenState extends State<OrderListScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  Map<String, dynamic> _initialValue = {};

  late QRTableProvider _qrTableProvider;
  late OrderProvider _orderProvider;

  RequestResult<Order>? orderListRequestResult;

  List<Order> orderList = [];
  List<QRTable> qrTableList = [];

  @override
  void didChangeDependencies() async {
    super.didChangeDependencies();

    _orderProvider = context.read<OrderProvider>();

    orderListRequestResult = await _orderProvider?.get(
        filter: {"orderStatus": OrderStatus.ACTIVE.name, "isOrderItemsIncluded": true});
    setState(() {
      orderList = orderListRequestResult!.result;
    });
  }

  @override
  void initState() {
    super.initState();
    _qrTableProvider = context.read<QRTableProvider>();

    _initialValue = {
      'orderStatus': OrderStatus.ACTIVE.name.toString(),
    };

    loadQRTableList();
  }

  Future loadQRTableList() async {
    var tmpQRTableList = await _qrTableProvider?.get();
    setState(() {
      qrTableList = tmpQRTableList!.result;
    });
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      child: Row(
        children: [
          Expanded(
            child: Container(
              child: Column(
                children: [_buildSearch(), _buildOrderListView()],
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildOrderListView() {
    return Expanded(
      child: GridView.count(
          primary: false,
          padding: const EdgeInsets.all(20),
          crossAxisSpacing: 50,
          mainAxisSpacing: 30,
          crossAxisCount: 4,
          children: _buildOrderListCards()),
    );
  }

  Widget _buildSearch() {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: FormBuilder(
        key: _formKey,
        initialValue: _initialValue,
        child: Row(
          children: [
            Expanded(
              child: FormBuilderDateTimePicker(
                name: "orderDateTimeFrom",
                decoration: InputDecoration(
                    labelText: "Datum od",
                    suffixIcon: IconButton(
                      icon: const Icon(Icons.close),
                      onPressed: () {
                        setState(() {
                          _formKey.currentState?.fields['orderDateTimeFrom']?.reset();
                        });
                      },
                    )),
                initialDate: DateTime.now(),
                firstDate: DateTime(2000),
                lastDate: DateTime.now(),
                inputType: InputType.date, // Set inputType to date
                format: DateFormat('dd.MM.yyyy'),
                onChanged: (value) {},
                onSaved: (value) {},
              ),
            ),
            SizedBox(
              width: 8,
            ),
            Expanded(
              child: FormBuilderDateTimePicker(
                name: "orderDateTimeTo",
                decoration: InputDecoration(
                    labelText: "Datum do",
                    suffixIcon: IconButton(
                      icon: const Icon(Icons.close),
                      onPressed: () {
                        setState(() {
                          _formKey.currentState?.fields['orderDateTimeTo']?.reset();
                        });
                      },
                    )),
                initialDate: DateTime.now(),
                firstDate: DateTime(2000),
                lastDate: DateTime.now(),
                inputType: InputType.date,
                format: DateFormat('dd.MM.yyyy'),
                onChanged: (value) {},
                onSaved: (value) {},
              ),
            ),
            SizedBox(
              width: 8,
            ),
            Expanded(
              child: FormBuilderDropdown<String>(
                name: 'orderStatus',
                decoration: InputDecoration(
                    labelText: "Status narudžbe",
                    suffixIcon: IconButton(
                      icon: const Icon(
                        Icons.close,
                      ),
                      onPressed: () {
                        _formKey.currentState!.fields['orderStatus']?.reset();
                      },
                    ),
                    hintText: "Odaberi status narudžbe"),
                items: [
                  DropdownMenuItem<String>(
                    value: OrderStatus.ACTIVE.name,
                    child: Text(OrderStatus.ACTIVE.value),
                  ),
                  DropdownMenuItem<String>(
                    value: OrderStatus.COMPLETED.name,
                    child: Text(OrderStatus.COMPLETED.value),
                  ),
                  DropdownMenuItem<String>(
                    value: OrderStatus.CANCELED.name,
                    child: Text(OrderStatus.CANCELED.value),
                  ),
                ],
              ),
            ),
            SizedBox(
              width: 8,
            ),
            ElevatedButton(
              onPressed: () async {
                _formKey.currentState?.saveAndValidate();
                var request = Map.from(_formKey.currentState!.value);
                request['isOrderItemsIncluded'] = true;
                var orderListSearchResult = await _orderProvider.get(filter: request);
                setState(() {
                  orderList = orderListSearchResult!.result;
                });
              },
              child: const Text("Pretraga"),
            ),
          ],
        ),
      ),
    );
  }

  List<Widget> _buildOrderListCards() {
    if (orderList.length == 0) {
      return [Text("Lista narudžbi je prazna.")];
    }

    List<Widget> list = orderList
        .map((x) => Container(
              child: Column(
                children: [
                  Material(
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(10),
                      side: const BorderSide(color: Colors.blue),
                    ),
                    color: Colors.blueAccent,
                    child: Padding(
                      padding: const EdgeInsets.all(9.0),
                      child: Column(
                        children: [
                          Container(
                              child: ListTile(
                            dense: true,
                            title: Text("Status narudžbe",
                                style: TextStyle(
                                    fontWeight: FontWeight.bold, color: Colors.black)),
                            trailing: Text(x.status!,
                                style: TextStyle(fontWeight: FontWeight.bold)),
                          )),
                          Container(
                              child: ListTile(
                            dense: true,
                            title: Text("STO br.:",
                                style: TextStyle(
                                  fontWeight: FontWeight.bold,
                                )),
                            trailing: Text("1",
                                style: TextStyle(
                                  fontWeight: FontWeight.bold,
                                )),
                          )),
                          Container(
                              child: ListTile(
                            dense: true,
                            title: Text("Vrijeme narudžbe:",
                                style: TextStyle(
                                    fontWeight: FontWeight.bold, color: Colors.black)),
                            trailing: Text(
                              DateFormat('dd.MM.yyyy HH:mm').format(x.orderDateTime!),
                              style: TextStyle(fontWeight: FontWeight.bold),
                            ),
                          )),
                          Container(
                              child: ListTile(
                            dense: true,
                            title: Text("PDV:",
                                style: TextStyle(
                                    fontWeight: FontWeight.bold, color: Colors.black)),
                            trailing: Text((x.vat! * 100).toInt().toString() + "%",
                                style: TextStyle(fontWeight: FontWeight.bold)),
                          )),
                          Container(
                              child: ListTile(
                            dense: true,
                            title: Text("Iznos (bez uračunatog PDV-a):",
                                style: TextStyle(
                                    fontWeight: FontWeight.bold, color: Colors.black)),
                            trailing: Text(formatNumber(x.totalAmount),
                                style: TextStyle(fontWeight: FontWeight.bold)),
                          )),
                          Container(
                              child: ListTile(
                            dense: true,
                            title: Text("Iznos:",
                                style: TextStyle(
                                    fontWeight: FontWeight.bold, color: Colors.black)),
                            trailing: Text(formatNumber(x.totalAmountWithVAT),
                                style: TextStyle(fontWeight: FontWeight.bold)),
                          )),
                          Padding(
                            padding: EdgeInsets.symmetric(vertical: 12.0),
                            child: Row(
                              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                              children: [
                                ElevatedButton(
                                  style: ElevatedButton.styleFrom(
                                    foregroundColor: Colors.blue,
                                    backgroundColor: Colors.white, // Text color
                                  ),
                                  onPressed: () async {
                                    // OTKAZII
                                  },
                                  child: const Text("Otkaži"),
                                ),
                                SizedBox(
                                  width: 10,
                                ),
                                ElevatedButton(
                                  style: ElevatedButton.styleFrom(
                                    foregroundColor: Colors.blue,
                                    backgroundColor: Colors.white, // Text color
                                  ),
                                  onPressed: () async {
                                    final items = x.orderItems.map((orderItem) {
                                      final name = orderItem.menuItem?.name ?? '';
                                      final unitPrice = orderItem.menuItem?.price ?? 0.0;
                                      final subtotal = orderItem.subtotal ?? 0.0;
                                      final quantity = orderItem.quantity ?? 0;

                                      return InvoiceItem(
                                          name: name,
                                          quantity: quantity,
                                          unitPrice: unitPrice,
                                          subtotal: subtotal);
                                    }).toList();

                                    final invoice = Invoice(
                                      supplier: Supplier(
                                          name: 'Caffe Pub - Skeniraj Plati',
                                          address: 'ul. Abdulaha Sidrana, Sarajevo, BiH',
                                          contactInfo: "+387 62 123 321"),
                                      info: InvoiceInfo(
                                        date: x.orderDateTime!,
                                        number: x.id.toString(),
                                      ),
                                      orderDateTime: x.orderDateTime!,
                                      totalAmount: x.totalAmount!,
                                      totalAmountWithVAT: x.totalAmountWithVAT!,
                                      vat: x.vat!,
                                      items: items,
                                    );

                                    // final pdfFile = await PdfInvoiceApi.generate(invoice);

                                    // PdfApi.openFile(pdfFile);
                                  },
                                  child: const Text("Kreiraj račun"),
                                )
                              ],
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                ],
              ),
            ))
        .cast<Widget>()
        .toList();

    return list;
  }
}
