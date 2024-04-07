// ignore_for_file: prefer_const_constructors, prefer_const_literals_to_create_immutables

import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:spsp_desktop/models/cart.dart';
import 'package:spsp_desktop/models/menu_item.dart';
import 'package:spsp_desktop/models/order.dart';
import 'package:spsp_desktop/models/order_item.dart';
import 'package:spsp_desktop/models/qr_table.dart';
import 'package:spsp_desktop/models/search_result.dart';
import 'package:spsp_desktop/providers/cart_provider.dart';
import 'package:spsp_desktop/providers/menu_item_provider.dart';
import 'package:spsp_desktop/providers/order_provider.dart';
import 'package:spsp_desktop/providers/qr_table_provider.dart';
import 'package:spsp_desktop/utils/util.dart';
import 'package:spsp_desktop/widgets/master_screen.dart';
import 'package:spsp_desktop/widgets/qr_table_screen.dart';

class POSScreen extends StatefulWidget {
  const POSScreen({super.key});

  @override
  State<POSScreen> createState() => _POSScreenState();
}

class _POSScreenState extends State<POSScreen> {
  late MenuItemProvider _menuItemProvider;
  late QRTableProvider _qrTableProvider;
  late OrderProvider _orderProvider;
  CartProvider? _cartProvider;

  SearchResult<MenuItem>? menuItemListResult;

  final TextEditingController _ftsController = TextEditingController();
  List<MenuItem> menuItemList = [];
  List<QRTable> qrTableList = [];
  // QRTable? selectedTableId;
  // TextEditingController _searchController = TextEditingController();

  // @override
  // void didChangeDependencies() async {
  //   super.didChangeDependencies();

  //   _menuItemProvider = context.read<MenuItemProvider>();

  //   var menuItemList = await _menuItemProvider.get();
  //   setState(() {
  //     menuItemListResult = menuItemList;
  //   });
  // }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _cartProvider = context.watch<CartProvider>();
  }

  @override
  void initState() {
    super.initState();
    _menuItemProvider = context.read<MenuItemProvider>();
    _qrTableProvider = context.read<QRTableProvider>();
    _orderProvider = context.read<OrderProvider>();
    print("called initState");
    loadMenuItemList();
    loadQRTableList();
  }

  Future loadMenuItemList() async {
    var tmpmenuItemList = await _menuItemProvider?.get();
    setState(() {
      menuItemList = tmpmenuItemList!.result;
    });
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
                children: [_buildSearch(), _buildmenuItemListListView()],
              ),
            ),
          ),
          SizedBox(
            width: MediaQuery.of(context).size.width * 0.2,
            child: Container(
              decoration: BoxDecoration(
                  border: Border.all(color: const Color.fromARGB(255, 0, 1, 2))),
              child: Column(
                children: [
                  Expanded(child: _buildOrderList()),
                  Container(
                    decoration: BoxDecoration(
                      border: Border(
                        top: BorderSide(
                          color: Colors.black,
                          width: 1,
                        ),
                      ),
                    ),
                    alignment: Alignment.bottomCenter,
                    child: ListTile(
                      onTap: () => showDialog<String>(
                        context: context,
                        builder: _buildQRTableDialog,
                      ),
                      title: Text("Sto br.:",
                          style: TextStyle(
                              fontStyle: FontStyle.italic, fontWeight: FontWeight.bold)),
                      trailing: _cartProvider?.cart.qrTable != null
                          ? Text(_cartProvider!.cart.qrTable!.tableNumber.toString(),
                              style: TextStyle(
                                  fontStyle: FontStyle.italic,
                                  fontWeight: FontWeight.bold))
                          : Text(""),
                    ),
                  ),
                  Container(
                    decoration: BoxDecoration(
                      border: Border(
                        top: BorderSide(
                          color: Colors.black,
                          width: 1,
                        ),
                      ),
                    ),
                    alignment: Alignment.bottomCenter,
                    child: ListTile(
                      title: Text("Cijena (sa PDV-om):",
                          style: TextStyle(
                              fontStyle: FontStyle.italic, fontWeight: FontWeight.bold)),
                      trailing: Text(_cartProvider!.cart.totalAmountWithVAT.toString(),
                          style: TextStyle(
                              fontStyle: FontStyle.italic, fontWeight: FontWeight.bold)),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildOrderList() {
    if (_cartProvider?.cart != null) {
      return Container(
        child: ListView.builder(
          itemCount: _cartProvider?.cart.items.length,
          itemBuilder: (context, index) {
            if (index == _cartProvider!.cart.items.length - 1) {
              return _buildOrderItemCard(
                  _cartProvider!.cart.items[index],
                  BoxDecoration(
                    border: Border(
                      bottom: BorderSide(
                        color: Colors.black,
                        width: 1,
                      ),
                    ),
                  ));
            } else {
              return _buildOrderItemCard(_cartProvider!.cart.items[index], null);
            }
          },
        ),
      );
    } else
      return Text("");
  }

  Widget _buildOrderItemCard(CartItem orderItem, BoxDecoration? decoration) {
    return Container(
      decoration: decoration,
      child: ListTile(
        leading: imageFromBase64String(orderItem.menuItem!.image!),
        title: Text(orderItem.menuItem?.name ?? ""),
        subtitle: Text(orderItem.menuItem!.price.toString()),
        trailing: Row(
          mainAxisSize: MainAxisSize.min,
          children: [
            Text(orderItem.quantity.toString()),
            const SizedBox(
              width: 20,
            ),
            IconButton(
              icon: Icon(Icons.delete),
              onPressed: () {
                _cartProvider?.removeFromCart(orderItem);
              },
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildmenuItemListListView() {
    return Expanded(
      child: GridView.count(
          primary: false,
          padding: const EdgeInsets.all(20),
          crossAxisSpacing: 10,
          mainAxisSpacing: 10,
          crossAxisCount: 5,
          children: _buildProductCardList()),
    );
  }

  Widget _buildSearch() {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: Row(
        children: [
          Expanded(
            child: TextField(
              decoration: const InputDecoration(
                labelText: "Naziv, šifra ili opis",
              ),
              controller: _ftsController,
            ),
          ),
          ElevatedButton(
            onPressed: () async {
              var menuItemListResult = await _menuItemProvider.get(filter: {
                'fts': _ftsController.text,
              });

              setState(() {
                menuItemList = menuItemListResult.result;
              });
            },
            child: const Text("Pretraga"),
          ),
          const SizedBox(width: 10),
          ElevatedButton(
            // onPressed: () => {showDialog<String>(
            //   context: context,
            //   builder: _buildQRTableDialog,
            // )},
            onPressed: () async {
              if (_cartProvider?.cart.qrTable == null) {
                showDialog<String>(
                  context: context,
                  builder: _buildQRTableDialog,
                );
              } else {
                var create = Order.fromCart(_cartProvider!.cart);
                await _orderProvider.create(create);
                _cartProvider!.cart = Cart();
                // Notify listeners after changing cart state
                _cartProvider!.notifyListeners();
              }
            },
            child: const Text("Kreiraj narudžbu"),
          ),
        ],
      ),
    );
  }

  Widget _buildQRTableDialog(BuildContext context) => Dialog.fullscreen(
        child: Center(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            // mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  const Text('ŠANK',
                      style: TextStyle(
                          fontStyle: FontStyle.italic, fontWeight: FontWeight.bold))
                ],
              ),
              const SizedBox(height: 25),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Transform.rotate(
                    angle: -90 * 3.1415926535 / 180,
                    child: Text(
                      'PROZORI',
                      style: TextStyle(
                          // fontStyle: FontStyle.italic,
                          fontWeight: FontWeight.bold,
                          fontSize: 16.0),
                    ),
                  ),
                  const SizedBox(width: 25),
                  TableWidget(
                    qrTableList: qrTableList,
                    onTap: (tableNumber) {
                      if (!qrTableList[tableNumber - 1].isTaken) {
                        print('Table $tableNumber tapped!');
                        setState(() {
                          _cartProvider?.cart.qrTable = qrTableList[tableNumber - 1];
                        });
                        Navigator.pop(context);
                      }
                    },
                  ),
                  const SizedBox(width: 25),
                  Transform.rotate(
                    angle: 90 * 3.1415926535 / 180,
                    child: Text(
                      'TOALET',
                      style: TextStyle(fontWeight: FontWeight.bold, fontSize: 16.0),
                    ),
                  ),
                ],
              ),
              const SizedBox(height: 25),
              Row(mainAxisAlignment: MainAxisAlignment.center, children: [
                ElevatedButton(
                  onPressed: () async {
                    Navigator.pop(context);

                    // var menuItemList = await _menuItemProvider.get(filter: {
                    //   'name': _ftsController.text,
                    // });
                    // setState(() {
                    //   menuItemListResult = menuItemList;
                    // });
                  },
                  child: const Text("Zatvori"),
                )
              ])
            ],
          ),
        ),
      );

  List<Widget> _buildProductCardList() {
    if (menuItemList.length == 0) {
      return [Text("Loading...")];
    }

    List<Widget> list = menuItemList
        .map((x) => Container(
              child: Column(
                children: [
                  Material(
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(10),
                      side: const BorderSide(color: Colors.blueAccent),
                    ),
                    color: Colors.blueAccent,
                    child: Column(
                      children: [
                        InkWell(
                          onTap: () {
                            _cartProvider?.addToCart(x);
                          },
                          child: Container(
                            height: 150,
                            width: 150,
                            child: imageFromBase64String(x.image!),
                          ),
                        ),
                        Text(x.name ?? ""),
                        Text(formatNumber(x.price))
                      ],
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
