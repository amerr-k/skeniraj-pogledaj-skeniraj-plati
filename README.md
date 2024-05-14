Lijep pozdrav.
Prije nego što pokrenete lokalno aplikaciju potrebno je izvršiti sljedeće korake:

- Korak 1.

U "Developer settings" u Windowsu uključiti "Developer mode" obzirom da se u desktop UI koristi biblioteku za snimanje na fajl sistem. Osim toga nisu potrebne nikakve dodatne izmjene jer aplikacija kreira folder u C: disk pod nazivom „invoices“ ukoliko ne postoji već. Tu se trenutno kreiraju i računi koje radnik manualno kreira i izvještaji za kupce i meni stavke.

![for_developers](https://github.com/amerr-k/skeniraj-pogledaj-skeniraj-plati/assets/25527072/b6cce689-25c0-479d-8c58-1bf903b75967)


- Korak 2.

Ukoliko planirate da mobilnu aplikaciju testirate emulatorom, trebate se pobrinuti da vaše postavke Android kamere u Android Studiu izgledaju ovako:

![android_postavke_1](https://github.com/amerr-k/skeniraj-pogledaj-skeniraj-plati/assets/25527072/101e2652-4c28-443c-8ed7-ff4bdceb6414)

![android_postavke_2](https://github.com/amerr-k/skeniraj-pogledaj-skeniraj-plati/assets/25527072/897023bc-3f15-4bb0-b5ee-1c39841205a8)

- Korak 3.

Da bi se testirao pregled i checkout narudžbi za određene stolove, potrebno je ubaciti slike QR kodova unutar Android okruženja prilikom pokretanja emulatora.

Slike QR kodova se nalaze u folderu ```/ui/spsp_mobile/qr_table_images/```. Stolovi br. 3, 4, 5 imaju aktivne narudžbe u bazi podataka koja se puni prilikom pokretanja API-ja te je tokom testiranja najbolje koristiti slike jednog od tri pomenuta stola.

Slike QR kodova se ubacuju na ovaj način:

![qr_code_slike_postavke_2](https://github.com/amerr-k/skeniraj-pogledaj-skeniraj-plati/assets/25527072/df45a5e7-3fef-4416-b9d1-1f172494a579)

QR kod staviti na „Wall“ kao na slici jer još nisam shvatio kako da kamera tokom korištenja okruženja postane viša.

Kroz okruženje kamere na emulatoru krećete se pomoću držanja **ALT-a + WASD + miš**

Skener nije baš 100% efikasan iz svih uglova slikanja i veličina slika. Meni najbolje radi ukoliko kao na slici smanjim veličinu slike na 0.2 i prilikom skeniranja pomjeram se hvatajući pravi ugao.

![qr_code_1](https://github.com/amerr-k/skeniraj-pogledaj-skeniraj-plati/assets/25527072/a0661a0e-b583-4c88-96df-29500f89be25) ![qr_code_2](https://github.com/amerr-k/skeniraj-pogledaj-skeniraj-plati/assets/25527072/1b2841f7-843e-44e6-9a8d-958f3500843c)

**POKRETANJE I BUILD API-ja**

```docker-compose up –build```

Da biste se uvjerili da EmailSubscriber koji šalje PDF generisane račune, radi očekivano, u konzoli će se svakih pet sekundi prikazivati poruka: ```"Listening for email messages."```

**KREDENCIJALI ZA GMAIL RAČUN NA KOJI SE ŠALJU PDF GENERISANI RAČUNI:**

Email: spsp.customer@gmail.com

Lozinka: poslana na **amel.music@edu.fit.ba i rs.ii@edu.fit.ba**

**ENV FAJL ZA PAY PAL SECRET_ID I CLIENT_ID:**

Prije builda UI mobile aplikacije potrebno je imati ```.env``` fajl u kojem se nalaze ```CLIENT_ID_VALUE``` i ```SECRET_ID_VALUE``` za PAYPAL GATEWAY.

```.env``` fajl sam poslao na sljedeće mailove: **amel.music@edu.fit.ba i rs.ii@edu.fit.ba**. Ukoliko mi DLWMS dozvoli, postavit ću i tu.

Fajl ```.env``` je potrebno kopirati u ```/spsp_mobile``` folder tj. odmah uz prisutne ```pubsec``` fajlove.

**POKRETANJE UI APLIKACIJA**

desktop: ```flutter run -d windows```

mobile: ```flutter run -d emulator-5554```

![emulator-5554](https://github.com/amerr-k/skeniraj-pogledaj-skeniraj-plati/assets/25527072/8c93cbc5-1ab2-418d-bd72-813eb0b2443d)

**KREDENCIJALI ZA PRIJAVU NA SANDBOX PAYPAL ACCOUNT ZA PLAĆANJE NARUDŽBI**

Username: sb-ysbl930441108@personal.example.com

Lozinka: poslana na amel.music@edu.fit.ba i rs.ii@edu.fit.ba

**KREDENCIJALI ZA PRIJAVU NA APLIKACIJU**

Username: radnik

Lozinka: test

Username: kupac

Lozinka: test

Username: kupac2

Lozinka: test

Username: kupac3

Lozinka: test






