<template>
  <q-page class="flex flex-center">
    <div>
      <h3>An interactive leaflet map</h3>
      <div id="map" style="height: 60vh; width: 75vw;"></div>
    </div>
  </q-page>
</template>

<script setup>
  import { ref, onMounted } from 'vue';
  import "leaflet/dist/leaflet.css";
  import * as L from 'leaflet';

  import 'leaflet.markercluster/dist/MarkerCluster.css';
  import 'leaflet.markercluster/dist/MarkerCluster.Default.css';
  import "leaflet.markercluster";

  const initialMap = ref(null);

  onMounted(() => {
    initialMap.value = L.map('map').setView([23.8041, 90.4152], 6);
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 19,
      attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(initialMap.value);

    //add marker
    //L.marker([24.3746, 88.6004]).addTo(initialMap.value);
    //L.marker([22.3752, 91.8349]).addTo(initialMap.value);

    const markers = L.markerClusterGroup();

    var addressPoints = [
      { latitude: 25.8031, longitude: 80.413 },
      { latitude: 25.8031, longitude: 88.413 },
      { latitude: 25.8031, longitude: 90.413 },
      { latitude: 25.8031, longitude: 95.413 },


      { latitude: 22.8031, longitude: 92.413 },
      { latitude: 21.8031, longitude: 91.413 },

      { latitude: 23.8031, longitude: 90.413 },
      { latitude: 23.8042, longitude: 90.414 },
      { latitude: 23.8043, longitude: 90.415 },
      { latitude: 23.8044, longitude: 90.411 },
      { latitude: 23.8040, longitude: 90.410 },
      { latitude: 23.8039, longitude: 90.416 },
      { latitude: 23.8045, longitude: 90.417 },
      { latitude: 23.8046, longitude: 90.417 },
      { latitude: 23.8047, longitude: 90.417 },
      { latitude: 23.8048, longitude: 90.417 },
      { latitude: 23.8049, longitude: 90.417 },

      { latitude: 23.8050, longitude: 90.410 },
      { latitude: 23.8051, longitude: 90.411 },
      { latitude: 23.8052, longitude: 90.412 },
      { latitude: 23.8053, longitude: 90.413 },
      { latitude: 23.8054, longitude: 90.414 },
      { latitude: 23.8055, longitude: 90.415 },
      { latitude: 23.8056, longitude: 90.416 },
      { latitude: 23.8057, longitude: 90.417 },
      { latitude: 23.8058, longitude: 90.418 },
      { latitude: 23.8059, longitude: 90.419 },

      { latitude: 23.8064, longitude: 90.420 },
      { latitude: 23.8061, longitude: 90.421 },
      { latitude: 23.8062, longitude: 90.422 },
      { latitude: 23.8063, longitude: 90.423 },
      { latitude: 23.8060, longitude: 90.424 },
      { latitude: 23.8065, longitude: 90.425 },
      { latitude: 23.8066, longitude: 90.426 },
      { latitude: 23.8067, longitude: 90.427 },
      { latitude: 23.8068, longitude: 90.428 },
      { latitude: 23.8069, longitude: 90.429 },

      { latitude: 23.8074, longitude: 90.435 },
      { latitude: 23.8071, longitude: 90.431 },
      { latitude: 23.8072, longitude: 90.432 },
      { latitude: 23.8073, longitude: 90.433 },
      { latitude: 23.8070, longitude: 90.434 },
      { latitude: 23.8075, longitude: 90.430 },
      { latitude: 23.8076, longitude: 90.436 },
      { latitude: 23.8077, longitude: 90.437 },
      { latitude: 23.8078, longitude: 90.438 },
      { latitude: 23.8079, longitude: 90.439 },

      { latitude: 23.8084, longitude: 90.445 },
      { latitude: 23.8081, longitude: 90.441 },
      { latitude: 23.8089, longitude: 90.449 },
      { latitude: 23.8083, longitude: 90.443 },
      { latitude: 23.8080, longitude: 90.444 },
      { latitude: 23.8085, longitude: 90.440 },
      { latitude: 23.8086, longitude: 90.446 },
      { latitude: 23.8087, longitude: 90.447 },
      { latitude: 23.8088, longitude: 90.448 },
      { latitude: 23.8082, longitude: 90.442 },

      { latitude: 23.8094, longitude: 90.455 },
      { latitude: 23.8091, longitude: 90.451 },
      { latitude: 23.8099, longitude: 90.459 },
      { latitude: 23.8093, longitude: 90.453 },
      { latitude: 23.8090, longitude: 90.454 },
      { latitude: 23.8095, longitude: 90.450 },
      { latitude: 23.8096, longitude: 90.456 },
      { latitude: 23.8097, longitude: 90.457 },
      { latitude: 23.8098, longitude: 90.458 },
      { latitude: 23.8092, longitude: 90.452 },
    ];

    addressPoints.forEach((element, index) => {
      const each_marker = new L.marker([element.latitude, element.longitude])
                              .bindPopup(`<strong> Hello Bangladesh! </strong> <br> I am a popup number ${index}`);;
      markers.addLayer(each_marker);
    });

    initialMap.value.addLayer(markers);
  });

</script>
