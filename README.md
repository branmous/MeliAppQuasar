# Operación Fuego de Quasar
## Brandon Steven Montoya Usuga

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

A continuación se realizará una descripción técnica de la solución desarrollada para la prueba técnica de MELI Fuego de Quasar

## Solución Teoria
Para la solución de la prueba se investigo la geolocalización por Trilateración la cual implica la medición de distancias.
esta consiste en tener tres satelites en diferentes puntos conocidos en el espacio, para el ejemplo nuestros los tres satelites tienen la siguentes posiciones:
- Kenobi: [-500, -200] 
- Skywalker: [100, -100] 
- Sato: [500, 100] 

![Screenshot](https://github.com/branmous/meli-back/blob/main/images/satellites.png?raw=true)

Cada Satelite emite una señal para su receptor conocida como distancia, esta forma un círculo igual en todas las direcciones, lo cual determina que la posicion podria estar en cualquier parte de este radio específico.
![Screenshot](https://github.com/branmous/meli-back/blob/main/images/satellitesradio.png?raw=true)

##Formula
De acuerdo al metodo matematico la formula es la siguiente
![Screenshot](https://github.com/branmous/meli-back/blob/main/images/formula.png?raw=true)


## Solcución Técnica
| Pre Requisitos |
| ------ |
| [Visual Studio 2022](https://code.visualstudio.com/download) | [Visual]|
| [Net6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) |

Nota: También se puede utilizar [Visual Studio Code](https://code.visualstudio.com/download)

## Ejecución
```sh
cd MeliBackQuasar
dotnet build
dotnet run
Open Browser http://localhost:5175/swagger/index.html
```

## Arquitectura
El proyecto está compuesto por una arquitectura de 3 Capas: Infraestructura, Dominio, Presentación en la cual se realizara una descripción de cada una de las capas
## Infraestructura 🔩
Esta capa comprende todo lo relacionado con el modelo de base de datos y los repositorios que generan todo el Crud.
Para este caso se creo un Modelo llamado Location el cual almacena la informacion de los satelites con la siguiente estrucutura:
| Nombre del campo | Tipo de dato |
| ------ | ------ |
| Name | String |
| Distance | Double |
| Message | Array String |

La base de datos que se utilizo para almacenar los datos se llama Datastore de Google App Engine 
![Screenshot](https://github.com/branmous/meli-back/blob/main/images/datastore.png?raw=true)


## Dominio 🔩
Esta capa comprende todo lo relacionado con la lógica que negocio del proyecto.
La cual tiene dos Servicios:
- LocationService: Este servicio está encargado de realizar toda la lógica para triangular la posición de la nave.
- MessageService: Este servicio se encarga de realizar la lógica para encontrar el mensaje de auxilio.

## Presentación 🛠️
Esta capa expone los endpoint encargados de recibir las peticiones de la nave portacarga.

 Servicios

* /topsecret/ - Este Servicio recibe el siguiente formato:
    ```
    {
      "satellites": [
        {
          "name": "kenobi",
          "distance": 100,
          "message": [
            "este", "", "", "mensaje", ""
          ]
        },
        {
          "name": "skywalker",
          "distance": 115.5,
          "message": [
            "", "es", "", "", "secreto"
          ]
        },
        {
          "name": "sato",
          "distance": 142.7,
          "message": [
            "este", "", "un", "", ""
          ]
        }
      ]
    }
    ```
    Respuesta Code 200
    ```
        {
        "position": {
            "x": -487,
            "y": 1557,
            "r": 0
        },
        "message": "este es un mensaje secreto "
    }
    ```
    Respuesta Code 400
        No hay información suficiente
    
* /topsecret_split/{name} - Este Servicio recibe el siguiente formato:
    ```
    {
      "distance": 100,
      "message": [
        "", "este","es","","mensaje"
      ]
    }
    ```
    
## Hosting
El proyecto se encuentra alojado en un Google App Engine utilizando la siguiente Guia [Create-app](https://cloud.google.com/appengine/docs/flexible/dotnet/create-app)

![Screenshot](https://github.com/branmous/meli-back/blob/main/images/projectName.png?raw=true)

## Docker

Para la publicación del proyecto se ejecutaron los siguientes comandos de docker

```sh
cd MeliBackQuasar
docker build -t mebackquasar-image  -f Dockerfile .
docker tag mebackquasar-image us-central1-docker.pkg.dev/meliappback/meli-app-api-repo/mebackquasar-image:latest
docker push us-central1-docker.pkg.dev/meliappback/meli-app-api-repo/mebackquasar-image:latest
```

## Proyecto

![Screenshot](https://github.com/branmous/meli-back/blob/main/images/projectv2.png?raw=true)



Puede ingresar dando click [aquí](https://mebackquasar-image-zeityctk4q-uc.a.run.app/swagger/index.html)

