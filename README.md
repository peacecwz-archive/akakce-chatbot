# Akakce API

Fiyat karşılaştırma sitesi olan Akakce.com u Crawler eden Bot API

# Geliştirme

Bu API ve Chatbot demosunu nasıl geliştirdiğimi merak edenler için Medium yazısını okuyabilirsiniz

![https://medium.com/@peacecwz/akakce-com-u-bot-framework-ile-chatbot-haline-getirelim-288f60320c8](https://medium.com/@peacecwz/akakce-com-u-bot-framework-ile-chatbot-haline-getirelim-288f60320c8)

# Kullanımı

## Search Endpoint

Request

```
curl -X GET \
  http://akakceapi.azurewebsites.net/api/product/search/{product-name}
```

Response

 ```
 [
    {
        "name": "Apple iPhone 6S 32GB Cep Telefonu",
        "imageUrl": "http://akakce.cubecdn.net/apple/apple-iphone-6s-32gb-x.jpg",
        "productUrl": "http://www.akakce.com/cep-telefonu/en-ucuz-apple-iphone-6s-32gb-fiyati,9249234.html"
    },
    {
        "name": "Apple iPhone 6S 16GB Cep Telefonu",
        "imageUrl": "http://akakce.cubecdn.net/apple/apple-iphone-6s-16gb-x.jpg",
        "productUrl": "http://www.akakce.com/cep-telefonu/en-ucuz-apple-iphone-6s-16gb-fiyati,5589613.html"
    },
    {
        "name": "Apple iPhone 6S Plus 32GB Cep Telefonu",
        "imageUrl": "http://akakce.cubecdn.net/apple/apple-iphone-6s-plus-32gb-x.jpg",
        "productUrl": "http://www.akakce.com/cep-telefonu/en-ucuz-apple-iphone-6s-plus-32gb-fiyati,9249247.html"
    },
    {
        "name": "Apple iPhone 6S 64GB Cep Telefonu",
        "imageUrl": "http://akakce.cubecdn.net/apple/apple-iphone-6s-64gb-x.jpg",
        "productUrl": "http://www.akakce.com/cep-telefonu/en-ucuz-apple-iphone-6s-64gb-fiyati,5665733.html"
    }
]
```

## Get Endpoint

Request

```
curl -X POST \
  http://akakceapi.azurewebsites.net/api/product/get \
  -H 'content-type: application/json' \
  -d '"http://www.akakce.com/cep-telefonu/en-ucuz-apple-iphone-6s-16gb-uzay-grisi-fiyati,6070378.html"'
```

Response

```
[
    {
        "name": "Apple iPhone 6S 16 GB (Apple Türkiye Garantili) - Space Gray",
        "price": "2.559,00 TL",
        "seller": "Hepsiburada",
        "url": "http://www.akakce.com/c/?c=17384&s=1&k=251&p=6070378&v=12088&f=%2Fr%2F%3Fpr%3D6070378%26vd%3D12088%26pg%3D173245546%26r%3Dhttp%253A%252F%252Fwww%252Ehepsiburada%252Ecom%252Fapple%252Diphone%252D6s%252D16%252Dgb%252Dapple%252Dturkiye%252Dgarantili%252Dp%252DTELCEPIPH6S16SI%252DN%253Fmagaza%253DSen%252520Cep",
        "imageUrl": "http://images.hepsiburada.net/assets/Telefon/200/Telefon_6991216.jpg"
    },
    {
        "name": "Apple iPhone 6S 16 GB - Uzay Grisi",
        "price": "2.749,00 TL",
        "seller": "Hepsiburada",
        "url": "http://www.akakce.com/c/?c=17384&s=2&k=3388&p=6070378&v=12088&f=%2Fr%2F%3Fpr%3D6070378%26vd%3D12088%26pg%3D173382579%26r%3Dhttp%253A%252F%252Fwww%252Ehepsiburada%252Ecom%252Fapple%252Diphone%252D6s%252D16%252Dgb%252Dithalatci%252Dgarantili%252Dp%252DTELCEPIPH6S16GR%252DSG%253Fmagaza%253DBsz%252520Elektronik",
        "imageUrl": "http://images.hepsiburada.net/assets/Telefon/200/Telefon_11910393.jpg"
    },
    {
        "name": "Apple iPhone 6S 16GB Space Gray (Siyah) Cep Telefonu - Apple Türkiye Garantili",
        "price": "2.999,00 TL",
        "seller": "Akakce",
        "url": "http://www.akakce.com/c/?c=17646&s=3&k=3084&p=6070378&v=9050&f=%2Fr%2F%3Fpr%3D6070378%26vd%3D9050%26pg%3D94158890%26r%3D%252Fsiparis%252F%253Fp%253D94158890",
        "imageUrl": "http://akakce.cubecdn.net/iv/9050/094/94158890x.jpg"
    }
]
```

# Akakce Chatbot

Akakce Crawler için yazılmış API ile oluşturulmuş örnek bir chatbot

# Demo

![](https://media.giphy.com/media/3o7aD1RbxUxhk6h3AA/giphy.gif)
