<template>
  <transition name="scroll-fade">
    <div v-show="visible" class="scroll-up-wrapper">
      <v-btn      
        elevation="6"
        class="scroll-up-btn"
        @click="scrollToTop"
        aria-hidden="true"
      >
        <v-icon size="48">mdi-chevron-up</v-icon>
      </v-btn>
    </div>
  </transition>
</template>

<script setup>
import { ref, onMounted, onBeforeUnmount } from 'vue'

const visible = ref(false)
const threshold = 100
let ticking = false

function onScroll() {
  if (!ticking) {
    window.requestAnimationFrame(() => {
      visible.value = window.scrollY > threshold
      ticking = false
    })
    ticking = true
  }
}

function scrollToTop() {
  window.scrollTo({ top: 0, behavior: 'smooth' })
}

onMounted(() => {
  window.addEventListener('scroll', onScroll, { passive: true })
  // initialize state
  visible.value = window.scrollY > threshold
})

onBeforeUnmount(() => {
  window.removeEventListener('scroll', onScroll)
})
</script>

<style scoped>
.scroll-up-wrapper {
  position: fixed;
  right: 1.25rem;
  bottom: 1.25rem;
  z-index: 2000;
}

.scroll-up-btn {
  width: 50px;
  height: 64px;
  border-radius: 50%;
  background-color: rgb(33, 33, 33);
  color:white;
  display: flex;
  align-items: center;
  justify-content: center;
}

/* Fade + slight move */
.scroll-fade-enter-active,
.scroll-fade-leave-active {
  transition: opacity .25s ease, transform .25s ease;
}
.scroll-fade-enter-from,
.scroll-fade-leave-to {
  opacity: 0;
  transform: translateY(8px);
}
.scroll-fade-enter-to,
.scroll-fade-leave-from {
  opacity: 1;
  transform: translateY(0);
}
</style>
