<template>
  <header class="py-4 px-2 bg-blue-400 flex flex-col gap-4 text-white font-bold">
    <div>
      <h1>Inkshelf</h1>
    </div>
    <div>
      <nav>
        <ul>
          <li><RouterLink :to="{ name: 'home' }">Accueil</RouterLink></li>
        </ul>
      </nav>
    </div>
  </header>
</template>
