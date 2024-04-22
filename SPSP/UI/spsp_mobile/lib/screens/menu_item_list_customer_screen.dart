import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:spsp_mobile/models/menu_item.dart';
import 'package:spsp_mobile/models/search_result.dart';
import 'package:spsp_mobile/providers/menu_item_provider.dart';
import 'package:spsp_mobile/providers/transaction_provider.dart';
import 'package:spsp_mobile/screens/menu_item_details_customer_screen.dart';
import 'package:spsp_mobile/utils/util.dart';
import 'package:spsp_mobile/widgets/master_screen.dart';

class MenuItemListCustomerScreen extends StatefulWidget {
  static const String routeName = "/menu-item";

  const MenuItemListCustomerScreen({super.key});

  @override
  State<MenuItemListCustomerScreen> createState() => _MenuItemListCustomerScreenState();
}

class _MenuItemListCustomerScreenState extends State<MenuItemListCustomerScreen> {
  RequestResult<MenuItem>? searchResult;
  late MenuItemProvider _menuItemProvider;
  final TextEditingController _ftsController = TextEditingController();
  final TextEditingController _nameController = TextEditingController();
  bool isLoading = true;

  @override
  void initState() {
    super.initState();

    _menuItemProvider = context.read<MenuItemProvider>();

    _loadMenuItems();
  }

  _loadMenuItems() async {
    var data = await _menuItemProvider.get();
    setState(() {
      searchResult = data;
      isLoading = false;
    });
  }

  @override
  void didChangeDependencies() async {
    super.didChangeDependencies();

    _menuItemProvider = context.read<MenuItemProvider>();
    var data = await _menuItemProvider.get();
    setState(() {
      searchResult = data;
    });
  }

  // @override
  // Widget build(BuildContext context) {
  //   return MasterScreenWidget(
  //     child: Container(
  //       child: Column(
  //         children: [_buildSearch(), _buildDataListView()],
  //       ),
  //     ),
  //   );
  // }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      child: Column(
        children: [
          _buildSearch(),
          Expanded(
              child: isLoading
                  ? Container(
                      child: Center(child: CircularProgressIndicator()),
                    )
                  : _buildProductCardList()),
        ],
      ),
    );
  }

  // Widget _buildDataListView() {
  //   return Expanded(
  //     child: SingleChildScrollView(
  //       child: isLoading ? Container() : _buildProductCardList(),
  //     ),
  //   );
  // }

  Widget _buildProductCardList() {
    return Container(
      child: ListView.builder(
        itemCount: searchResult?.count,
        itemBuilder: (context, index) {
          return _buildProductCard(searchResult!.result[index]);
        },
      ),
    );
  }

  Widget _buildProductCard(MenuItem item) {
    return ListTile(
      onTap: () {
        Navigator.pushNamed(
            context, "${MenuItemDetailsCustomerScreen.routeName}/${item.id}");
      },
      leading: imageFromBase64String(item!.image!),
      title: Text(item.name ?? ""),
      subtitle: Text(item.price.toString()),
      trailing: Text(item.price.toString()),
    );
  }

  // Widget _buildDataTable() {
  //   return Row(
  //     children: [
  //       Expanded(
  //         child: DataTable(
  //             showCheckboxColumn: false,
  //             columns: const [
  //               DataColumn(
  //                 label: Expanded(
  //                   child: Text(
  //                     'Id',
  //                     style: TextStyle(fontStyle: FontStyle.italic),
  //                   ),
  //                 ),
  //               ),
  //               DataColumn(
  //                 label: Expanded(
  //                   child: Text(
  //                     "Šifra",
  //                     style: TextStyle(fontStyle: FontStyle.italic),
  //                   ),
  //                 ),
  //               ),
  //               DataColumn(
  //                 label: Expanded(
  //                   child: Text(
  //                     'Naziv',
  //                     style: TextStyle(fontStyle: FontStyle.italic),
  //                   ),
  //                 ),
  //               ),
  //               DataColumn(
  //                 label: Expanded(
  //                   child: Text(
  //                     'Opis',
  //                     style: TextStyle(fontStyle: FontStyle.italic),
  //                   ),
  //                 ),
  //               ),
  //               DataColumn(
  //                 label: Expanded(
  //                   child: Text(
  //                     'Cijena',
  //                     style: TextStyle(fontStyle: FontStyle.italic),
  //                   ),
  //                 ),
  //               ),
  //               DataColumn(
  //                 label: Expanded(
  //                   child: Text(
  //                     'Slika',
  //                     style: TextStyle(fontStyle: FontStyle.italic),
  //                   ),
  //                 ),
  //               )
  //             ],
  //             rows: searchResult?.result
  //                     .map(
  //                       (e) => DataRow(
  //                           onSelectChanged: (selected) => {
  //                                 if (selected == true)
  //                                   {
  //                                     Navigator.of(context).push(
  //                                       MaterialPageRoute(
  //                                         builder: (context) => MenuItemDetailScreen(
  //                                           menuItem: e,
  //                                         ),
  //                                       ),
  //                                     )
  //                                   }
  //                               },
  //                           cells: [
  //                             DataCell(
  //                               Text(e.id?.toString() ?? ""),
  //                             ),
  //                             DataCell(
  //                               Text(e.code ?? ""),
  //                             ),
  //                             DataCell(
  //                               Text(e.name ?? ""),
  //                             ),
  //                             DataCell(
  //                               Text(e.description ?? ""),
  //                             ),
  //                             DataCell(
  //                               Text(formatNumber(e.price)),
  //                             ),
  //                             DataCell(
  //                               Container(
  //                                 width: 50,
  //                                 height: 50,
  //                                 decoration: BoxDecoration(
  //                                   border: Border.all(
  //                                     color: Colors.black,
  //                                     width: 1,
  //                                   ),
  //                                 ),
  //                                 child: imageFromBase64String(e.image!),
  //                               ),
  //                             ),
  //                           ]),
  //                     )
  //                     .toList() ??
  //                 []),
  //       ),
  //     ],
  //   );
  // }

  Widget _buildSearch() {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: Row(
        children: [
          Expanded(
            child: TextField(
              decoration: const InputDecoration(
                labelText: "Pretražite po nazivu",
              ),
              controller: _ftsController,
            ),
          ),
          const SizedBox(width: 8),
          Container(
            padding: EdgeInsets.symmetric(horizontal: 20, vertical: 10),
            child: IconButton(
              icon: Icon(Icons.filter_list),
              onPressed: () async {
                var tmpData =
                    // await _productProvider?.get({'naziv': _searchController.text});
                    setState(() {
                  // data = tmpData!;
                });
              },
            ),
          ),
          ElevatedButton(
            onPressed: () async {
              var data = await _menuItemProvider.get(filter: {
                'fts': _ftsController.text,
                'name': _nameController.text,
              });
              setState(() {
                searchResult = data;
              });
            },
            child: const Text("Pretraga"),
          ),
          const SizedBox(width: 8),
        ],
      ),
    );
  }
}
