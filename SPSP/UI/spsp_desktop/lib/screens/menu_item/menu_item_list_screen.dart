import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:spsp_desktop/models/menu_item/menu_item.dart';
import 'package:spsp_desktop/models/search_result.dart';
import 'package:spsp_desktop/providers/menu_item_provider.dart';
import 'package:spsp_desktop/screens/menu_item/menu_item_detail_screen.dart';
import 'package:spsp_desktop/utils/util.dart';
import 'package:spsp_desktop/widgets/master_screen.dart';

class MenuItemListScreen extends StatefulWidget {
  int? menuId;
  MenuItemListScreen({Key? key, this.menuId}) : super(key: key);
  @override
  State<MenuItemListScreen> createState() => _MenuItemListScreenState();
}

class _MenuItemListScreenState extends State<MenuItemListScreen> {
  RequestResult<MenuItem>? searchResult;
  late MenuItemProvider _menuItemProvider;
  final TextEditingController _ftsController = TextEditingController();
  final TextEditingController _nameController = TextEditingController();

  @override
  void initState() {
    super.initState();
    _menuItemProvider = context.read<MenuItemProvider>();
    _fetchData();
  }

  Future<void> _fetchData() async {
    RequestResult<MenuItem> data;
    if (widget.menuId != null) {
      data = await _menuItemProvider.get(filter: {
        "menuId": widget.menuId,
      });
    } else {
      data = await _menuItemProvider.get(filter: {
        "isMenuActive": true,
      });
    }

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
              // horizontalMargin: 100,
              columnSpacing: 150,
              columns: const [
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
              rows: searchResult?.result.asMap().entries.map(
                    (entry) {
                      int index = entry.key;
                      var e = entry.value;

                      return DataRow(
                        onSelectChanged: (selected) {
                          if (selected == true) {
                            Navigator.of(context).push(
                              MaterialPageRoute(
                                builder: (context) => MenuItemDetailScreen(
                                  menuItem: e,
                                ),
                              ),
                            );
                          }
                        },
                        cells: [
                          DataCell(Text(e.code ?? "")),
                          DataCell(Text(e.name ?? "")),
                          DataCell(Text(e.description ?? "")),
                          DataCell(Text(formatNumber(e.price))),
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
                        ],
                      );
                    },
                  ).toList() ??
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
                labelText: "Naziv, šifra ili opis",
              ),
              controller: _ftsController,
            ),
          ),
          const SizedBox(width: 8),
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
              var filter = {
                'fts': _ftsController.text,
                'name': _nameController.text,
              };

              if (widget.menuId != null) {
                filter['menuId'] = widget.menuId.toString();
              } else {
                filter['isMenuActive'] = "true";
              }

              var data = await _menuItemProvider.get(filter: filter);

              setState(() {
                searchResult = data;
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
            child: const Text("Kreiraj novu meni stavku"),
          ),
        ],
      ),
    );
  }
}
