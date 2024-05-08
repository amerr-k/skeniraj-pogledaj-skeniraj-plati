import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:spsp_desktop/models/customer_report_data.dart';
import 'package:spsp_desktop/models/menu_item_report_data.dart';
import 'package:spsp_desktop/models/report.dart';
import 'package:spsp_desktop/providers/base_provider.dart';

class ReportProvider extends BaseProvider<Report> {
  ReportProvider() : super("Report");

  @override
  Report fromJson(data) {
    return Report.fromJson(data);
  }

  Future<List<MenuItemReportData>> getMenuItemsReportData({dynamic filter}) async {
    var url = "${baseUrl}$endpoint/MenuItems";
    if (filter != null) {
      var queryString = getQueryString(filter);
      url = "$url?$queryString";
    }
    var uri = Uri.parse(url);
    var headers = createHeaders();

    var response = await http!.get(uri, headers: headers);

    if (isValidResponse(response)) {
      var data = jsonDecode(response.body);

      return data
          .map((x) => MenuItemReportData.fromJson(x))
          .cast<MenuItemReportData>()
          .toList();
    } else {
      throw new Exception("Unknown error");
    }
  }

  Future<List<CustomerReportData>> getCustomersReportData({dynamic filter}) async {
    var url = "${baseUrl}$endpoint/Customers";
    if (filter != null) {
      var queryString = getQueryString(filter);
      url = "$url?$queryString";
    }
    var uri = Uri.parse(url);
    var headers = createHeaders();

    var response = await http!.get(uri, headers: headers);

    if (isValidResponse(response)) {
      var data = jsonDecode(response.body);

      return data
          .map((x) => CustomerReportData.fromJson(x))
          .cast<CustomerReportData>()
          .toList();
    } else {
      throw new Exception("Unknown error");
    }
  }
  // Future<List<BestAccomodations>> getBestAccomodations(int agencyId) async {
  //   Map<String, String> headers = await createHeaders();

  //   var response = await http?.get(
  //       Uri.parse("${baseUrl}Reservation/GetBestAccomodations/$agencyId"),
  //       headers: headers);

  //   if (response!.body.isNotEmpty) {
  //     var data = jsonDecode(response.body);

  //     return data
  //         .map((x) => BestAccomodations.fromJson(x))
  //         .cast<BestAccomodations>()
  //         .toList();
  //   } else {
  //     print("Error here");
  //     return List<BestAccomodations>.empty();
  //   }
  // }
}
