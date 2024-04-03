import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';
import 'package:http/http.dart';
import 'package:spsp_desktop/models/menu_item.dart';
import 'package:spsp_desktop/models/search_result.dart';
import 'package:spsp_desktop/utils/util.dart';

class MenuItemProvider with ChangeNotifier {
  static String? _baseUrl;
  final String _endpoint = "MenuItem"; //ovo promjeni
  MenuItemProvider() {
    _baseUrl =
        const String.fromEnvironment("baseUrl", defaultValue: "http://localhost:7011/");
  }
//kad opcionalne parametre hoces poslat stavis u viticaste zagrade
  Future<SearchResult<MenuItem>> get({dynamic filter}) async {
    var url = "$_baseUrl$_endpoint";

    if (filter != null) {
      var queryString = getQueryString(filter);
      url = "$url?$queryString";
    }

    print(filter);
    print(url);

    var uri = Uri.parse(url);
    var headers = createHeaders();
    var response = await http.get(uri, headers: headers);

    if (isValidResponse(response)) {
      var jsonData = jsonDecode(response.body);

      var searchResult = SearchResult<MenuItem>();

      searchResult.count = jsonData["count"];

      for (var menuItem in jsonData['result']) {
        // MenuItem menuItem = MenuItem();
        // menuItem.id = item['id'];
        // menuItem.name = item['name'];
        searchResult.result.add(MenuItem.fromJson(menuItem));
      }

      return searchResult;
    } else {
      print("NOT-VALID");

      throw new Exception("Unknown error");
    }
  }

  bool isValidResponse(Response response) {
    if (response.statusCode < 299) {
      return true;
    } else if (response.statusCode == 401) {
      throw new Exception("Unauthorized");
    } else {
      print("response.body");
      print(response.body);
      throw new Exception("Something bad happened. Please try again.");
    }
  }

  Map<String, String> createHeaders() {
    String username = Authorization.username ?? "";
    String password = Authorization.password ?? "";

    String basicAuth = "Basic ${base64Encode(
      utf8.encode('$username:$password'),
    )}";

    var headers = {"Content-Type": "application/json", "Authorization": basicAuth};

    return headers;
  }

  String getQueryString(Map params, {String prefix = '&', bool inRecursion = false}) {
    String query = '';
    params.forEach((key, value) {
      if (inRecursion) {
        if (key is int) {
          key = '[$key]';
        } else if (value is List || value is Map) {
          key = '.$key';
        } else {
          key = '.$key';
        }
      }
      if (value is String || value is int || value is double || value is bool) {
        var encoded = value;
        if (value is String) {
          encoded = Uri.encodeComponent(value);
        }
        query += '$prefix$key=$encoded';
      } else if (value is DateTime) {
        query += '$prefix$key=${(value as DateTime).toIso8601String()}';
      } else if (value is List || value is Map) {
        if (value is List) value = value.asMap();
        value.forEach((k, v) {
          query += getQueryString({k: v}, prefix: '$prefix$key', inRecursion: true);
        });
      }
    });
    return query;
  }
}
