<template>
  <header class="py-4 px-2 bg-gray-200 flex flex-col gap-4">
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
