<template>
  <footer class="px-2 py-4 flex flex-row justify-between bg-gray-200">
    <div>
      <RouterLink :to="{ name: 'home' }">InkShelf</RouterLink>
    </div>
    <div>
      <p>
        Contacte nous
        <a href="mailto:cyrilauquier@hotmail.fr" class="underline font-semibold p-2">ici</a> .
      </p>
    </div>
  </footer>
</template>
