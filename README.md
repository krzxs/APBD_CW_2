# Uczelniany system wypożyczalni sprzętu

## Opis:

Konsolowa aplikacja do zarządzania wypożyczeniami urządzeń (laptopy, kamery, projektory) przez pracowników i studentów.

## Podział kodu i odpowiedzialność klas:

### Warstwa Domain:
Klasy biznesowe: Device (i podtypy), User (i podtypy), Rental, BaseObject (abstrakcyjna klasa odpowiedzialna za generowanie ID)

### Warstwa Repositories:
Dostęp do danych w pamięci (klasy DeviceRepository, UserRepository, RentalRepository), zero logiki biznesowej

### Warstwa Services:
Logika biznesowa aplikacji (klasy RentalService, DeviceService, UserService, ReportService, DataService)

### Warstwa Config:
Wydzielone reguły biznesowe (klasa RentalRules)

### Warstwa DTOs:
Klasy służące do serializacji/deserializacji danych (klasy DeviceDto, RentalDto, UserDto)

### Warstwa Exceptions:
Własne klasy wyjątków

### Warstwa UI:
Klasa odpowiedzialna za warstwę prezentacji aplikacji oraz obsługująca wejście/wyjście (ConsoleMenu)

## Uzasadnienie podziału kodu:
Podział kodu na warstwy sprawił, że każda klasa ma jedno jasne zadanie.\
Klasa ConsoleMenu to jedyne miejsce, które obsługuje w projekcie obsługujące wejście/wyjście. Zastąpienie tej klasy innym interfejsem np. HTTP REST API, nie spowoduje zmiany w logice biznesowej.\
Repozytoria zajmują się wyłącznie przechowywaniem obiektów, bez żadnej logiki biznesowej.\
Logika wypożyczeń znajduje się w klasie RentalService, która koordynuje ze sobą klasy UserService i DeviceService, bez sięgania do ich repozytoriów.\
Wyliczanie kary zostało wydzielone do RentalRules, aby zmiana dziennych stawek nie wymagała modyfikacji serwisów.\
Klasy warstwy DTOs oddzielają format zapisu danych od klas domenowych, dzięki czemu warstwa domenowa nie musi wiedzieć nic o serializacji.

## Kohezja:
Każda klasa w systemie ma dobrze zdefiniowaną odpowiedzialność. Na przykład:
- RentalService zajmuje się wyłącznie logiką wypożyczeń,
- RentalRules zajmuje się tylko obliczaniem kary za spóźnienie, gdyby jednak zasady się zmieniły, zmiana dotyczy tylko jednego miejsca w systemie,
- Rental jako klasa domenowa sam wylicza czy jest aktywny lub spóźniony, nie przerzuca tej logiki na serwis.

## Coupling:
- Serwisy otrzymują zależności przez konstruktor, dzięki czemu nie tworzą ich samodzielnie i nie są z nimi silnie związane,
- Klasa ConsoleMenu zależy tylko od serwisów, nigdy od repozytoriów, tj. warstwa UI nie wie jak dane są przechowywane,
- DataService komunikuje się z domeną przez DTOs, tj. klasy takie jak Rental czy Device nie wiedzą nic o serializacji.