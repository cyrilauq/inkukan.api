<script setup lang="ts"></script>
<template>
  <div class="flex flex-col gap-3 bg-blue-100 py-4 px-2 text-justify rounded-lg">
    <section>
      <h2>Bienvenue sur <span class="font-bold">InkShelf</span>!</h2>
      <p>Grâce à nous tu pourras gérer en toute tranquillité votre collection de mangas.</p>
    </section>
    <section>
      <p>Tu veux garder un oeil sur ta collection de manga?</p>
      <p>Tu veux arrêter d'acheter des mangas en doubles?</p>
      <p>Viens nous rejoindre en t'inscrivant</p>
      <button class="bg-blue-600 rounded-lg text-white px-2 py-1">==> S'inscrire</button>
    </section>
    <section>
      <h2>Newsletter</h2>
      <p>
        Tu veux être au courant des dernier changement de l'app ou des dernières nouveautés mangas?
      </p>
      <p>Inscris-toi à notre newsletter!</p>
      <form class="flex flex-row">
        <input type="email" name="email" id="email" class="email" placeholder="Ton mail" />
        <button>S'inscrire</button>
      </form>
    </section>
  </div>
</template>
<style lang="css" scoped></style>
