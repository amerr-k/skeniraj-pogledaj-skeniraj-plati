// ignore_for_file: prefer_const_constructors, prefer_const_literals_to_create_immutables

import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:provider/provider.dart';
import 'package:spsp_desktop/models/menu_item.dart';
import 'package:spsp_desktop/models/search_result.dart';
import 'package:spsp_desktop/providers/menu_item_provider.dart';
import 'package:spsp_desktop/screens/menu_item_detail_screen.dart';
import 'package:spsp_desktop/utils/util.dart';
import 'package:spsp_desktop/widgets/master_screen.dart';

class POSScreen extends StatefulWidget {
  const POSScreen({super.key});

  @override
  State<POSScreen> createState() => _POSScreenState();
}

class _POSScreenState extends State<POSScreen> {
  SearchResult<MenuItem>? menuItemListResult;
  late MenuItemProvider _menuItemProvider;
  //samo po nazivu cu pretrazivat
  final TextEditingController _nameController = TextEditingController();

  List<MenuItem> data = [];

  // @override
  // void didChangeDependencies() async {
  //   super.didChangeDependencies();

  //   _menuItemProvider = context.read<MenuItemProvider>();

  //   var data = await _menuItemProvider.get();
  //   setState(() {
  //     menuItemListResult = data;
  //   });
  // }

  TextEditingController _searchController = TextEditingController();

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    _menuItemProvider = context.read<MenuItemProvider>();
    print("called initState");
    loadData();
  }

  Future loadData() async {
    var tmpData = await _menuItemProvider?.get();
    setState(() {
      data = tmpData!.result;
    });
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      child: Container(
        child: Column(
          children: [_buildSearch(), _buildDataListView()],
        ),
      ),
    );
  }

  Widget _buildDataListView() {
    return Expanded(
      child: SingleChildScrollView(
        child: Row(
          children: [_buildGridView(), _buildBillBox()],
        ),
      ),
    );
  }

  Widget _buildBillBox() {
    return Container(
        margin: const EdgeInsets.all(15.0),
        padding: const EdgeInsets.all(15.0),
        decoration: BoxDecoration(border: Border.all(color: Colors.blueAccent)),
        height: 50,
        width: 50,
        child: Text("asdasd"));
  }

  Widget _buildGridView() {
    return Container(
      margin: const EdgeInsets.all(5.0),
      padding: const EdgeInsets.all(5.0),
      // decoration: BoxDecoration(border: Border.all(color: Colors.blueAccent)),
      height: 600,
      width: 1100,
      child: GridView(
        scrollDirection: Axis.vertical,
        gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
            crossAxisCount: 4,
            childAspectRatio: 4 / 3,
            crossAxisSpacing: 20,
            mainAxisSpacing: 30),
        children: _buildProductCardList(),
      ),
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
                labelText: "Naziv",
              ),
              controller: _nameController,
            ),
          ),
          ElevatedButton(
            onPressed: () async {
              var data = await _menuItemProvider.get(filter: {
                'name': _nameController.text,
              });
              setState(() {
                menuItemListResult = data;
              });
            },
            child: const Text("Pretraga"),
          ),
          const SizedBox(width: 8),
          ElevatedButton(
            onPressed: () async {
              Navigator.of(context).push(
                MaterialPageRoute(
                  builder: (context) => MenuItemDetailScreen(),
                ),
              );
            },
            child: const Text("Kreiraj narudžbu"),
          ),
        ],
      ),
    );
  }

  List<Widget> _buildProductCardList() {
    if (data.length == 0) {
      return [Text("Loading...")];
    }

    List<Widget> list = data
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
                            // Navigator.pushNamed(
                            //     context, "${ProductDetailsScreen.routeName}/${x.proizvodId}");
                            // _cartProvider?.addToCart(x);
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




// DataTable(
//               showCheckboxColumn: false,
//               columns: const [
//                 DataColumn(
//                   label: Expanded(
//                     child: Text(
//                       'Id',
//                       style: TextStyle(fontStyle: FontStyle.italic),
//                     ),
//                   ),
//                 ),
//                 DataColumn(
//                   label: Expanded(
//                     child: Text(
//                       "Šifra",
//                       style: TextStyle(fontStyle: FontStyle.italic),
//                     ),
//                   ),
//                 ),
//                 DataColumn(
//                   label: Expanded(
//                     child: Text(
//                       'Naziv',
//                       style: TextStyle(fontStyle: FontStyle.italic),
//                     ),
//                   ),
//                 ),
//                 DataColumn(
//                   label: Expanded(
//                     child: Text(
//                       'Opis',
//                       style: TextStyle(fontStyle: FontStyle.italic),
//                     ),
//                   ),
//                 ),
//                 DataColumn(
//                   label: Expanded(
//                     child: Text(
//                       'Cijena',
//                       style: TextStyle(fontStyle: FontStyle.italic),
//                     ),
//                   ),
//                 ),
//                 DataColumn(
//                   label: Expanded(
//                     child: Text(
//                       'Slika',
//                       style: TextStyle(fontStyle: FontStyle.italic),
//                     ),
//                   ),
//                 )
//               ],
//               rows: menuItemListResult?.result
//                       .map(
//                         (e) => DataRow(
//                             onSelectChanged: (selected) => {
//                                   if (selected == true)
//                                     {
//                                       Navigator.of(context).push(
//                                         MaterialPageRoute(
//                                           builder: (context) => MenuItemDetailScreen(
//                                             menuItem: e,
//                                           ),
//                                         ),
//                                       )
//                                     }
//                                 },
//                             cells: [
//                               DataCell(
//                                 Text(e.id?.toString() ?? ""),
//                               ),
//                               DataCell(
//                                 Text(e.code ?? ""),
//                               ),
//                               DataCell(
//                                 Text(e.name ?? ""),
//                               ),
//                               DataCell(
//                                 Text(e.description ?? ""),
//                               ),
//                               DataCell(
//                                 Text(formatNumber(e.price)),
//                               ),
//                               DataCell(
//                                 Container(
//                                   width: 50,
//                                   height: 50,
//                                   decoration: BoxDecoration(
//                                     border: Border.all(
//                                       color: Colors.black,
//                                       width: 1,
//                                     ),
//                                   ),
//                                   child: imageFromBase64String(e.image!),
//                                 ),
//                               ),
//                             ]),
//                       )
//                       .toList() ??
//                   [])