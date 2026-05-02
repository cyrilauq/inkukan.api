<script setup lang="ts"></script>
<template>
  <div class="flex flex-col gap-2">
    <section>
      <h2>Bienvenue sur <span class="font-bold">InkShelf</span>!</h2>
      <p>Ici vous pourrez gérer en toute tranquillité votre collection de mangas.</p>
    </section>
    <section>
      <h2>Fonctionnalité</h2>
      <p>Grâce à notre application vous pourrez:</p>
      <ul>
        <li>Gérer votre collection de manga</li>
        <li>Avoir une vue sur les mangas qui sortiront</li>
        <li>Avoir une suggestion personnalisée de mangas</li>
        <li>Et bien d'autre</li>
      </ul>
    </section>
    <section>
      <h2>Envie d'en savoir plus?</h2>
      <p>Viens nous rejoindre en t'inscrivant</p>
      <button>S'inscrire</button>
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
