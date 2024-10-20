<script>
  import NavBar from "$lib/components/NavBar.svelte";
</script>

<div class="w-full h-full flex flex-col justify-between px-5 bg-neutral-100 bg-topo">
    <main class="my-10 h-[calc(100% + 10rem)] flex flex-col justify-center align-middle items-center">
        <slot></slot>
    </main>

    <NavBar/>
</div>
