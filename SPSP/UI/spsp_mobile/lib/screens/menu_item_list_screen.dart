import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:spsp_mobile/models/menu_item.dart';
import 'package:spsp_mobile/models/search_result.dart';
import 'package:spsp_mobile/providers/menu_item_provider.dart';
import 'package:spsp_mobile/screens/menu_item_detail_screen.dart';
import 'package:spsp_mobile/utils/util.dart';
import 'package:spsp_mobile/widgets/master_screen.dart';

class MenuItemListScreen extends StatefulWidget {
  static const String routeName = "/menu-item";

  const MenuItemListScreen({super.key});

  @override
  State<MenuItemListScreen> createState() => _MenuItemListScreenState();
}

class _MenuItemListScreenState extends State<MenuItemListScreen> {
  RequestResult<MenuItem>? searchResult;
  late MenuItemProvider _menuItemProvider;
  final TextEditingController _ftsController = TextEditingController();
  final TextEditingController _nameController = TextEditingController();

  @override
  void didChangeDependencies() async {
    super.didChangeDependencies();

    _menuItemProvider = context.read<MenuItemProvider>();

    var data = await _menuItemProvider.get();
    setState(() {
      searchResult = data;
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
        child: _buildDataTable(),
      ),
    );
  }

  Widget _buildDataTable() {
    return Row(
      children: [
        Expanded(
          child: DataTable(
              showCheckboxColumn: false,
              columns: const [
                DataColumn(
                  label: Expanded(
                    child: Text(
                      'Id',
                      style: TextStyle(fontStyle: FontStyle.italic),
                    ),
                  ),
                ),
                DataColumn(
                  label: Expanded(
                    child: Text(
                      "Šifra",
                      style: TextStyle(fontStyle: FontStyle.italic),
                    ),
                  ),
                ),
                DataColumn(
                  label: Expanded(
                    child: Text(
                      'Naziv',
                      style: TextStyle(fontStyle: FontStyle.italic),
                    ),
                  ),
                ),
                DataColumn(
                  label: Expanded(
                    child: Text(
                      'Opis',
                      style: TextStyle(fontStyle: FontStyle.italic),
                    ),
                  ),
                ),
                DataColumn(
                  label: Expanded(
                    child: Text(
                      'Cijena',
                      style: TextStyle(fontStyle: FontStyle.italic),
                    ),
                  ),
                ),
                DataColumn(
                  label: Expanded(
                    child: Text(
                      'Slika',
                      style: TextStyle(fontStyle: FontStyle.italic),
                    ),
                  ),
                )
              ],
              rows: searchResult?.result
                      .map(
                        (e) => DataRow(
                            onSelectChanged: (selected) => {
                                  if (selected == true)
                                    {
                                      Navigator.of(context).push(
                                        MaterialPageRoute(
                                          builder: (context) => MenuItemDetailScreen(
                                            menuItem: e,
                                          ),
                                        ),
                                      )
                                    }
                                },
                            cells: [
                              DataCell(
                                Text(e.id?.toString() ?? ""),
                              ),
                              DataCell(
                                Text(e.code ?? ""),
                              ),
                              DataCell(
                                Text(e.name ?? ""),
                              ),
                              DataCell(
                                Text(e.description ?? ""),
                              ),
                              DataCell(
                                Text(formatNumber(e.price)),
                              ),
                              DataCell(
                                Container(
                                  width: 50,
                                  height: 50,
                                  decoration: BoxDecoration(
                                    border: Border.all(
                                      color: Colors.black,
                                      width: 1,
                                    ),
                                  ),
                                  child: imageFromBase64String(e.image!),
                                ),
                              ),
                            ]),
                      )
                      .toList() ??
                  []),
        ),
      ],
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
                labelText: "Naziv ili opis proizvoda",
              ),
              controller: _ftsController,
            ),
          ),
          const SizedBox(width: 8),
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
          )
        ],
      ),
    );
  }
}
