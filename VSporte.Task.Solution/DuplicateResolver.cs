using VSporte.Task.API.DTOs;
using VSporte.Task.Solution.Models;

namespace VSporte.Task.Solution;

public class DuplicateResolver
{
    /* Нужно избавиться от дублей игроков и клубов.Связка игрока с клубом проверяться не будет
    Дублем игрока считается игрок, у которого одни и те же имя, фамилия и номер.
    Дублем клуба считается клуб с тем же названием клуба и городом.
    Основной сущностью среди дублей, считается первая сущность по порядку. Основная сущность остается, остальные дубли удаляются.
    Количество дублей не ограничено. */
    public Fingerprint Resolve(Fingerprint fingerprint)
    {
        // Унификация входных данных
        var formatFingerprint = FormatData((Fingerprint)fingerprint.Clone());

        var playerClubs = formatFingerprint.PlayerClubs;
        var uniqPlayerClubs = new List<PlayerClubItem>();

        // Создание списков без дублей
        var uniqPlayers = formatFingerprint.Players.DistinctBy(p => new { p.Name, p.Surname, p.Number });
        var uniqClubs = formatFingerprint.Clubs.DistinctBy(c => new { c.FullName, c.City });

        // Переустановка связей игроков и клубов
        foreach (var pc in playerClubs)
        {
            // Добавляем связь игроков с клубом
            if (uniqPlayers.Select(up => up.PlayerId).Contains(pc.PlayerId)
                && uniqClubs.Select(uc => uc.ClubId).Contains(pc.ClubId))
                uniqPlayerClubs.Add(pc);

            // Добавляем связь игроков без клуба
            else if (uniqPlayers.Select(up => up.PlayerId).Contains(pc.PlayerId)
                && !uniqClubs.Select(uc => uc.ClubId).Contains(pc.ClubId)
                && !uniqPlayerClubs.Select(x => x.PlayerId).Contains(pc.PlayerId))
                uniqPlayerClubs.Add(new PlayerClubItem { PlayerId = pc.PlayerId });
        }

        return ReformatData(fingerprint, new Fingerprint
        {
            Players = uniqPlayers.ToArray(),
            Clubs = uniqClubs.ToArray(),
            PlayerClubs = uniqPlayerClubs.ToArray(),
        });
    }

    /* Нужно избавиться от дублей игроков и клубов. Связка игрока с клубом проверяться не будет
    Дублем игрока считаются игроки подходящие под один из двух пунктов:

    у которых одни и те же имя, фамилия и номер
    у которых одни и те же имя, фамилия и находятся в том же клубе

    Дублем клуба считается клуб с тем же названием клуба и города (если у клуба не задан город, то его нужно объединять с тем клубом у которого тоже название, но есть город, если есть два клуба с тем же названием и у обоих задан город, приоритет отдается клубу идущем первым в списке)
    Основной сущностью среди дублей, считается первая сущность по порядку. Основная сущность остается, остальные дубли удаляются.
    Количество дублей не ограничено. */
    public Fingerprint Resolve2(Fingerprint fingerprint)
    {
        // Унификация входных данных
        var formatFingerprint = FormatData((Fingerprint)fingerprint.Clone());

        var playerClubs = formatFingerprint.PlayerClubs;
        var uniqPlayerClubs = new List<PlayerClubItem>();

        var uniqPlayers = new List<PlayerWithClubIdDto>();

        var clubs = formatFingerprint.Clubs;
        var uniqClubs = new List<ClubItem>();

        var dtoPlayers = JoinClubIdToPlayer(playerClubs, formatFingerprint.Players);

        // Создание списка игроков без дублей
        foreach (var dtoPlayer in dtoPlayers)
        {
            var uniqPlayer = uniqPlayers
                .Where(up => up.Name == dtoPlayer.Name && up.Surname == dtoPlayer.Surname
                && (up.Number == dtoPlayer.Number || up.ClubId == dtoPlayer.ClubId))
                .SingleOrDefault();

            if (uniqPlayer == null)
                uniqPlayers.Add(dtoPlayer);
        }

        // Создание списка клубов без дублей
        for (int i = 0; i < clubs.Length; i++)
        {
            var uniqClub = uniqClubs
                .Where(dp => dp.FullName == clubs[i].FullName)
                .SingleOrDefault();

            if (uniqClub == null)
                uniqClubs.Add(clubs[i]);

            else if (uniqClub.City == null && clubs[i].City != null)
            {
                uniqClubs.Remove(uniqClub);
                uniqClubs.Add(clubs[i]);
            }
        }

        // Переустановка связей игроков и клубов
        foreach (var pc in playerClubs)
        {
            // Добавляем связь игроков с клубом
            if (uniqPlayers.Select(up => up.PlayerId).Contains(pc.PlayerId)
                && uniqClubs.Select(uc => uc.ClubId).Contains(pc.ClubId))
                uniqPlayerClubs.Add(pc);

            // Добавляем связь игроков без клуба
            else if (uniqPlayers.Select(up => up.PlayerId).Contains(pc.PlayerId)
                && !uniqClubs.Select(uc => uc.ClubId).Contains(pc.ClubId)
                && !uniqPlayerClubs.Select(x => x.PlayerId).Contains(pc.PlayerId))
                uniqPlayerClubs.Add(new PlayerClubItem { PlayerId = pc.PlayerId });
        }

        return ReformatData(fingerprint, new Fingerprint
        {
            Players = uniqPlayers.Select(up => new PlayerItem
            {
                Name = up.Name,
                Surname = up.Surname,
                Number = up.Number,
                PlayerId = up.PlayerId
            }).ToArray(),
            Clubs = uniqClubs.ToArray(),
            PlayerClubs = uniqPlayerClubs.ToArray(),
        });
    }


    /* Нужно избавиться от дублей игроков и клубов.
    Связка игрока с клубом проверяется в этом задании: игроки из дубля клуба связываются с основным клубом
    Дублем игрока считаются игроки подходящие под один из двух пунктов:

    у которых одни и те же имя, фамилия и номер
    у которых одни и те же имя, фамилия и находятся в том же клубе

    Дублем клуба считается клуб с тем же названием клуба и города(если у клуба не задан город, то его нужно объединять с тем клубом у которого тоже название, но есть город, если есть два клуба с тем же названием и у обоих задан город, приоритет отдается клубу идущем первым в списке)
    Основной сущностью среди дублей, считается первая сущность по порядку. Основная сущность остается, остальные дубли удаляются.
    Количество дублей не ограничено. */
    public Fingerprint Resolve3(Fingerprint fingerprint)
    {
        // Унификация входных данных
        var formatFingerprint = FormatData((Fingerprint)fingerprint.Clone());

        var playerClubs = formatFingerprint.PlayerClubs;
        var uniqPlayerClubs = new List<PlayerClubItem>();

        var uniqPlayers = new List<PlayerWithClubIdDto>();

        var clubs = formatFingerprint.Clubs;
        var uniqClubs = new List<ClubItem>();

        // Соединение игрока с id клуба
        var dtoPlayers = JoinClubIdToPlayer(playerClubs, formatFingerprint.Players);

        // Создание списков игроков и клубов без дублей
        var dtoClubs = dtoPlayers.GroupBy(p => p.ClubId);
        foreach (var dtoClub in dtoClubs)
        {
            var club = clubs.Where(c => c.ClubId == dtoClub.Key).SingleOrDefault();

            if (club != null)
            {
                var uniqClub = uniqClubs
                    .Where(dp => dp.FullName == club.FullName)
                    .SingleOrDefault();

                if (uniqClub == null)
                {
                    uniqClubs.Add(club);

                    foreach (var dtoPlayer in dtoClub)
                    {
                        // Поиск игрока с таким же именем и фамилией в списке без дублей
                        // Если такой игрок есть, то он должен быть только один
                        var dpUniqPlayer = uniqPlayers
                            .Where(up => up.Name == dtoPlayer.Name && up.Surname == dtoPlayer.Surname
                            && up.Number == dtoPlayer.Number && up.ClubId != dtoPlayer.ClubId)
                            .SingleOrDefault();

                        // Добавление игрока с такими же именем, фамилией и номером, но в другой клуб
                        if (dpUniqPlayer != null)
                        {
                            uniqPlayerClubs.Add(new PlayerClubItem
                            {
                                ClubId = dtoPlayer.ClubId,
                                PlayerId = dpUniqPlayer.PlayerId
                            });
                        }

                        // Поиск дублей внутри одного клуба
                        var uniqPlayer = uniqPlayers
                            .Where(up => up.Name == dtoPlayer.Name && up.Surname == dtoPlayer.Surname
                            && up.ClubId == dtoPlayer.ClubId)
                            .SingleOrDefault();

                        if (uniqPlayer == null && dpUniqPlayer == null)
                        {
                            uniqPlayers.Add(dtoPlayer);

                            uniqPlayerClubs.Add(new PlayerClubItem
                            {
                                ClubId = dtoPlayer.ClubId,
                                PlayerId = dtoPlayer.PlayerId
                            });
                        }
                    }
                }

                else if (uniqClub.City == null && club.City != null)
                {
                    // Замена на дублирующий клуб, но с заполненным городом
                    uniqClubs.Remove(uniqClub);
                    uniqClubs.Add(club);

                    // Установка нового клуба (с городом) для игроков из удаленного клуба
                    uniqPlayers
                        .FindAll(up => up.ClubId == uniqClub.ClubId)
                        .ForEach(up => up.ClubId = club.ClubId);

                    foreach (var dtoPlayer in dtoClub)
                    {
                        var uniqPlayer = uniqPlayers
                            .Where(up => up.Name == dtoPlayer.Name && up.Surname == dtoPlayer.Surname
                            && up.ClubId != dtoPlayer.ClubId)
                            .SingleOrDefault();

                        if (uniqPlayer == null)
                        {
                            uniqPlayers.Add(new PlayerWithClubIdDto
                            {
                                Name = dtoPlayer.Name,
                                Surname = dtoPlayer.Surname,
                                Number = dtoPlayer.Number,
                                PlayerId = dtoPlayer.PlayerId,
                                ClubId = uniqClub.ClubId
                            });

                            uniqPlayerClubs.Add(new PlayerClubItem
                            {
                                ClubId = uniqClub.ClubId,
                                PlayerId = dtoPlayer.PlayerId
                            });
                        }
                    }
                }

                else
                {
                    // Перенос игроков из дублирующего клуба
                    foreach (var dtoPlayer in dtoClub)
                    {
                        var uniqPlayer = uniqPlayers
                            .Where(up => up.Name == dtoPlayer.Name && up.Surname == dtoPlayer.Surname
                            && club.FullName == uniqClub.FullName)
                            .SingleOrDefault();

                        if (uniqPlayer == null)
                        {
                            uniqPlayers.Add(new PlayerWithClubIdDto
                            {
                                Name = dtoPlayer.Name,
                                Surname = dtoPlayer.Surname,
                                Number = dtoPlayer.Number,
                                PlayerId = dtoPlayer.PlayerId,
                                ClubId = uniqClub.ClubId
                            });

                            uniqPlayerClubs.Add(new PlayerClubItem
                            {
                                ClubId = uniqClub.ClubId,
                                PlayerId = dtoPlayer.PlayerId
                            });
                        }
                    }
                }
            }
        }

        return ReformatData(fingerprint, new Fingerprint
        {
            Players = uniqPlayers.Select(up => new PlayerItem
            {
                Name = up.Name,
                Surname = up.Surname,
                Number = up.Number,
                PlayerId = up.PlayerId
            }).ToArray(),
            Clubs = uniqClubs.ToArray(),
            PlayerClubs = uniqPlayerClubs.ToArray(),
        });
    }

    private static string ToCorrectFormat(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        return text
                .Trim()
                .ToLower()
                .Replace("ё", "e")
                .Replace("й", "и");
    }

    private static Fingerprint FormatData(Fingerprint fingerprint)
    {
        for (int i = 0; i < fingerprint.Players.Length; i++)
        {
            fingerprint.Players[i].Name = ToCorrectFormat(fingerprint.Players[i].Name);
            fingerprint.Players[i].Surname = ToCorrectFormat(fingerprint.Players[i].Surname);
        }

        for (int i = 0; i < fingerprint.Clubs.Length; i++)
        {
            fingerprint.Clubs[i].FullName = ToCorrectFormat(fingerprint.Clubs[i].FullName);
            fingerprint.Clubs[i].City = fingerprint.Clubs[i].City == null ? null : ToCorrectFormat(fingerprint.Clubs[i].City);
        }

        return fingerprint;
    }

    // Соединение игрока с id клуба
    private static List<PlayerWithClubIdDto> JoinClubIdToPlayer(PlayerClubItem[] playerClubs, PlayerItem[] players)
    {
        var dtoPlayers = new List<PlayerWithClubIdDto>();
        for (int i = 0; i < playerClubs.Length; i++)
        {
            var player = players.Where(p => p.PlayerId == playerClubs[i].PlayerId).SingleOrDefault();

            if (player != null)
                dtoPlayers.Add(new PlayerWithClubIdDto
                {
                    Name = player.Name,
                    Surname = player.Surname,
                    Number = player.Number,
                    PlayerId = player.PlayerId,
                    ClubId = playerClubs[i].ClubId
                });
        }

        return dtoPlayers;
    }

    // Возвращение данных к первоначальному виду
    private Fingerprint ReformatData(Fingerprint fingerprint, Fingerprint formatFingerprint)
    {
        foreach (var item in formatFingerprint.Players)
        {
            var player = fingerprint.Players.Where(p => p.PlayerId == item.PlayerId).SingleOrDefault();
            if (player != null)
            {
                item.Name = player.Name;
                item.Surname = player.Surname;
            }
        }

        foreach (var item in formatFingerprint.Clubs)
        {
            var club = fingerprint.Clubs.Where(p => p.ClubId == item.ClubId).SingleOrDefault();
            if (club != null)
            {
                item.FullName = club.FullName;
                item.City = club.City ?? null;
            }
        }

        return formatFingerprint;
    }
}